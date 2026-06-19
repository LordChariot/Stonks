using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Stonks
{
    class TwelveDataClient
    {
        private readonly string _apiKey;
        private readonly HttpClient _http;

        public TwelveDataClient(string apiKey)
        {
            _apiKey = apiKey;
            _http = new HttpClient();
        }

        private string GetJsonValue(string json, string key)
        {
            if (string.IsNullOrEmpty(json)) return null;
            // support dotted path like "fifty_two_week.high"
            if (key.Contains('.'))
            {
                var parts = key.Split('.');
                // find parent object
                var parent = parts[0];
                var idx = json.IndexOf('"' + parent + '"', StringComparison.OrdinalIgnoreCase);
                if (idx < 0) return null;
                var colon = json.IndexOf(':', idx);
                if (colon < 0) return null;
                // find start of object
                var objStart = json.IndexOf('{', colon);
                if (objStart < 0) return null;
                // find matching closing brace
                int depth = 0;
                int j = objStart;
                for (; j < json.Length; j++)
                {
                    if (json[j] == '{') depth++;
                    else if (json[j] == '}')
                    {
                        depth--;
                        if (depth == 0) break;
                    }
                }
                if (j >= json.Length) return null;
                var sub = json.Substring(objStart, j - objStart + 1);
                var remainder = string.Join(".", parts, 1, parts.Length - 1);
                return GetJsonValue(sub, remainder);
            }

            var idx2 = json.IndexOf('"' + key + '"', StringComparison.OrdinalIgnoreCase);
            if (idx2 < 0) return null;
            var colon2 = json.IndexOf(':', idx2);
            if (colon2 < 0) return null;
            var i = colon2 + 1;
            while (i < json.Length && char.IsWhiteSpace(json[i])) i++;
            if (i >= json.Length) return null;
            if (json[i] == '"')
            {
                var start = i + 1;
                var end = json.IndexOf('"', start);
                if (end > start) return json.Substring(start, end - start);
                return null;
            }
            else
            {
                // read unquoted token (number/null/true/false)
                var start = i;
                while (i < json.Length &&
                       (char.IsDigit(json[i]) || json[i] == '-' || json[i] == '+' || json[i] == '.' || json[i] == 'e' || json[i] == 'E'))
                {
                    i++;
                }
                if (i > start) return json.Substring(start, i - start);
                return null;
            }
        }

        public async Task<QuoteResult> GetQuoteAsync(string symbol, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(_apiKey)) throw new InvalidOperationException("API key not configured");
            var uri = $"https://api.twelvedata.com/quote?symbol={Uri.EscapeDataString(symbol)}&apikey={Uri.EscapeDataString(_apiKey)}";
            Logger.LogDebug($"GET {MaskUri(uri)}");
            var resp = await _http.GetAsync(uri, token);
            var body = await resp.Content.ReadAsStringAsync();
            Logger.LogDebug(body);
            if (resp.StatusCode == (System.Net.HttpStatusCode)429)
            {
                // look for Retry-After header
                int retry = 60;
                if (resp.Headers.TryGetValues("Retry-After", out var values))
                {
                    var v = values.FirstOrDefault();
                    if (int.TryParse(v, out var rr)) retry = rr;
                }
                throw new RateLimitException("Rate limited", retry);
            }
            if (!resp.IsSuccessStatusCode) throw new HttpRequestException($"HTTP {resp.StatusCode}");

            // Lightweight JSON parsing to avoid external dependencies.
            var code = GetJsonValue(body, "code");
            var message = GetJsonValue(body, "message");
            if (!string.IsNullOrEmpty(code) || !string.IsNullOrEmpty(message))
            {
                Logger.LogError($"API error: code={code}, message={message}");
                throw new Exception(message ?? "API error");
            }

            var high52 = GetJsonValue(body, "fifty_two_week.high");
            var low52 = GetJsonValue(body, "fifty_two_week.low");
            var nameStr = GetJsonValue(body, "name");
            var percentStr = GetJsonValue(body, "percent_change");
            var priceStr = GetJsonValue(body, "close");

            decimal.TryParse(high52, out var high);
            decimal.TryParse(low52, out var low);
            decimal.TryParse(percentStr, out var percent);
            decimal.TryParse(priceStr, out var price);

            return new QuoteResult
            {
                Price = price,
                PercentChange = percent,
                FiftyTwoWeekHigh = high,
                FiftyTwoWeekLow = low
                ,
                Name = nameStr
            };
        }

        private string MaskUri(string uri)
        {
            // naive mask of apikey
            var idx = uri.IndexOf("apikey=", StringComparison.OrdinalIgnoreCase);
            if (idx < 0) return uri;
            var start = idx + "apikey=".Length;
            var end = uri.IndexOf('&', start);
            if (end < 0) end = uri.Length;
            return uri.Substring(0, start) + "***" + uri.Substring(end);
        }

        public class QuoteResult
        {
            public decimal FiftyTwoWeekHigh { get; set; }
            public decimal FiftyTwoWeekLow { get; set; }
            public decimal PercentChange { get; set; }
            public decimal Price { get; set; }
            public string Name { get; set; }
        }

        internal class RateLimitException : Exception
        {
            public int RetryAfterSeconds { get; }
            public RateLimitException(string message, int retryAfterSeconds) : base(message) { RetryAfterSeconds = retryAfterSeconds; }
        }


    }
}
