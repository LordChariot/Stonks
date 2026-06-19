using System;
using System.Configuration;
using System.IO;

namespace Stonks
{
    static class Logger
    {
        private static readonly object _writeLock = new object();

        private static readonly string DefaultLogDir = Path.Combine(Path.GetDirectoryName(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath), "logs");
        public static string LogFile = Path.Combine(GetLogDir(), DateTime.Now.ToString("yyyy-MM-dd") + ".log");

        private static readonly string DefaultChartDir = Path.Combine(Path.GetDirectoryName(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath), "charts");
        public static string ChartDataFile = Path.Combine(GetChartDir(), $"{DateTime.Now:yyyy-MM-dd}.csv");

        private static string GetChartDir()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.ChartDirectory)) return Properties.Settings.Default.ChartDirectory;
            }
            catch { }
            return DefaultChartDir;
        }

        private static string GetLogDir()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.LogDirectory)) return Properties.Settings.Default.LogDirectory;
            }
            catch { }
            return DefaultLogDir;
        }

        public static void LogChartData(string time, string symbol, decimal currentPrice, decimal percentChange, decimal shares, decimal value, decimal pricePaid, decimal gain, decimal gainPercent, decimal fiftyTwoWeekHigh, decimal fiftyTwoWeekLow)
        {
            try
            {
                lock (_writeLock)
                {
                    var chartDir = GetChartDir();
                    try { Directory.CreateDirectory(chartDir); } catch { chartDir = DefaultChartDir; try { Directory.CreateDirectory(chartDir); } catch { } }
                    var path = ChartDataFile;
                    if (!File.Exists(path))
                    {
                        File.WriteAllText(path, "Time,Symbol,CurrentPrice,PercentChange,Shares,Value,PricePaid,Gain,GainPercent,FiftyTwoWeekHigh,FiftyTwoWeekLow" + Environment.NewLine);
                    }
                    File.AppendAllText(path, $"\"{time}\",\"{symbol}\",{currentPrice},{percentChange},{shares},{value},{pricePaid},{gain},{gainPercent},{fiftyTwoWeekHigh},{fiftyTwoWeekLow}{Environment.NewLine}");
                    LogDebug($"Chart data logged for {symbol} at {time}");
                }
            }
            catch { LogError("LogChartData: Failed to write chart data."); }
        }

        public static void LogDebug(string msg)
        {
            if (!Properties.Settings.Default.VerboseLogging) return;
            Write("DEBUG", msg);
        }

        public static void LogError(string msg) => Write("ERROR", msg);

        public static void LogInfo(string msg) => Write("INFO", msg);

        private static void Write(string level, string msg)
        {
            try
            {
                lock (_writeLock)
                {
                    var _logDir = GetLogDir();
                    try { Directory.CreateDirectory(_logDir); } catch { _logDir = DefaultLogDir; try { Directory.CreateDirectory(_logDir); } catch { } }
                    var _logFilePath = LogFile = Path.Combine(_logDir, DateTime.UtcNow.ToString("yyyy-MM-dd") + ".log");
                    File.AppendAllText(_logFilePath, $"[{DateTime.UtcNow:O}] {level}: {msg}{Environment.NewLine}");
                }
            }
            catch { LogError("Write: Failed to write log entry."); }
        }
    }
}
