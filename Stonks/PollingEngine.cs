using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Stonks
{
    public class PollingEngine
    {
        private CancellationTokenSource _cts;
        private Task _loopTask;
        private readonly object _lock = new object();
        private bool _isRunning;

        public event EventHandler<UpdateEventArgs> OnUpdate;
        public event EventHandler<string> OnNotification;

        public class UpdateEventArgs : EventArgs
        {
            public List<StockViewModel> Stocks { get; set; }
        }

        public void Start()
        {
            lock (_lock)
            {
                if (_isRunning) return;
                _isRunning = true;
                _cts = new CancellationTokenSource();
                _loopTask = Task.Run(() => LoopAsync(_cts.Token));
            }
        }

        public void Stop()
        {
            lock (_lock)
            {
                if (!_isRunning) return;
                _cts.Cancel();
                try { _loopTask.Wait(5000); } catch { }
                _isRunning = false;
            }
        }

        public async Task RefreshNowAsync()
        {
            await RunOnceAsync(CancellationToken.None);
        }

        private async Task LoopAsync(CancellationToken token)
        {
            var intervalMinutes = Math.Max(15, Properties.Settings.Default.PollingInterval);
            var backoff = 1;
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await RunOnceAsync(token);
                    backoff = 1;
                }
                catch (OperationCanceledException) { break; }
                catch (Exception ex)
                {
                    backoff = Math.Min(60, backoff * 2);
                    Logger.LogError("Polling error: " + ex.Message);
                }

                // wait interval
                for (int i = 0; i < intervalMinutes && !token.IsCancellationRequested; i++)
                {
                    await Task.Delay(TimeSpan.FromMinutes(1), token);
                }

                // apply backoff as extra minutes
                if (backoff > 1)
                {
                    try { await Task.Delay(TimeSpan.FromMinutes(backoff), token); } catch { }
                }
            }
        }

        private async Task RunOnceAsync(CancellationToken token)
        {
            var crypto = new Crypto() { EncryptedApiKey = Properties.Settings.Default.EncryptedApiKey };
            var _ApiKey = crypto.ApiKey;
            if (string.IsNullOrWhiteSpace(_ApiKey))
            {
                Logger.LogError($"ApiKey not defined");
                OnNotification?.Invoke(this, $"ApiKey not defined");
                return;
            }

            var stockList = new List<StockDefinition>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<StockDefinition>));
            using (var reader = new StringReader(Properties.Settings.Default.StockDefinitions))
            {
                stockList = (List<StockDefinition>)serializer.Deserialize(reader);
            }
            var symbols = stockList.Select(s => s.Symbol).ToList();
            if (!symbols.Any()) return;


            var client = new TwelveDataClient(_ApiKey);
            var chartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            var tasks = symbols.Select(async symbol =>
            {
                try
                {
                    var quote = await client.GetQuoteAsync(symbol, token);
                    var s = stockList.FirstOrDefault(x => x.Symbol == symbol);
                    return new StockViewModel
                    {
                        Symbol = symbol,
                        Name = quote?.Name ?? string.Empty,
                        Shares = s?.Shares ?? 0,
                        PricePaid = s?.PricePaid ?? 0m,
                        CurrentPrice = quote?.Price ?? 0m,
                        PercentChange = quote?.PercentChange ?? 0m,
                        FiftyTwoWeekHigh = quote?.FiftyTwoWeekHigh ?? 0m,
                        FiftyTwoWeekLow = quote?.FiftyTwoWeekLow ?? 0m,
                        Notes = s?.Notes ?? string.Empty,
                        Chart = s?.Chart ?? false
                    };
                }
                catch (TwelveDataClient.RateLimitException rl)
                {
                    // bubble up notification and trigger a backoff
                    Logger.LogError($"Rate limit while fetching {symbol}, retry after {rl.RetryAfterSeconds}s");
                    OnNotification?.Invoke(this, $"Rate limited: retry after {rl.RetryAfterSeconds}s");
                    throw; // allow outer loop to apply backoff
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error fetching {symbol}: {ex.Message}");
                    OnNotification?.Invoke(this, $"Error fetching {symbol}: {ex.Message}");
                    return new StockViewModel { Symbol = symbol };
                }
            });

            var results = await Task.WhenAll(tasks);
            OnUpdate?.Invoke(this, new UpdateEventArgs { Stocks = results.ToList() });
        }
    }
}
