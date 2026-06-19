using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Stonks
{
    public partial class MainForm : Form
    {
        private Series _highlightedSeries;
        private readonly Dictionary<Series, int> _origBorderWidth = new Dictionary<Series, int>();
        private readonly Dictionary<Series, int> _origMarkerSize = new Dictionary<Series, int>();

        public MainForm()
        {
            InitializeComponent();

            Logger.LogDebug("MainForm ctor: created");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // if window is minimized or maximized, save restore bounds so layout remains sane
                var _bounds = (this.WindowState == FormWindowState.Normal) ? this.Bounds : this.RestoreBounds;
                Properties.Settings.Default.WindowBounds = _bounds;

                // save column widths as comma-separated string, with sanity checks
                var _columns = dataGridView_StockList.Columns.Cast<DataGridViewColumn>().Select(c => Math.Max(20, Math.Min(2000, c.Width)));
                Properties.Settings.Default.ColumnWidths = string.Join(",", _columns);

                Logger.LogDebug($"SaveLayout: writing WindowBounds -> {Properties.Settings.Default.WindowBounds}");
                Logger.LogDebug($"SaveLayout: writing ColumnWidths -> {Properties.Settings.Default.ColumnWidths}");

                Properties.Settings.Default.Save();
            }
            catch { }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                Logger.LogDebug("LoadLayout: starting");

                this.Bounds = Properties.Settings.Default.WindowBounds;
                Logger.LogDebug($"LoadLayout: applied bounds  L={this.Left} T={this.Top} W={this.Width} H={this.Height}");

                var _widths = Properties.Settings.Default.ColumnWidths?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries) ?? new string[0];
                Logger.LogDebug($"LoadLayout: reading column widths: {string.Join(",", _widths)}");

                var _widthList = new List<int>();
                for (int i = 0; i < _widths.Length && i < dataGridView_StockList.Columns.Count; i++)
                {
                    if (int.TryParse(_widths[i], out var _width)) _widthList.Add(_width);
                    else _widthList.Add(0);
                }


                for (int i = 0; i < _widthList.Count; i++)
                {
                    var w = Math.Max(20, Math.Min(2000, _widthList[i]));
                    dataGridView_StockList.Columns[i].Width = w;
                }
                Logger.LogDebug($"MainForm_Load: applied column widths sum={string.Join(",", _widthList)}");


                foreach (var _chartValue in Enum.GetValues(typeof(ChartValues)))
                {
                    toolStripComboBox_ChartValues.Items.Add(_chartValue.ToString());
                }
                toolStripComboBox_ChartValues.SelectedIndex = Properties.Settings.Default.ChartValues;

                toolStripStatusLabel_Value.Width = toolStripStatusLabel_Gain.Width = toolStripStatusLabel_Paid.Width = (statusStrip_1.Width - 20) / 3;
                Logger.LogDebug("MainForm_Load: completed");
            }
            catch { }

            UpdateStockList(TrayApplicationContext.Stocks);

            if (chart.Visible)
            {
                try { GetChartData(); } catch { Logger.LogError("MainForm_Load: Failed to update chart data"); }
            }
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            toolStripStatusLabel_Value.Width = toolStripStatusLabel_Gain.Width = toolStripStatusLabel_Paid.Width = (statusStrip_1.Width - 20) / 3;
        }

        private void Timer_Chart_Tick(object sender, EventArgs e)
        {
            try { GetChartData(); } catch { Logger.LogError($"Timer_Chart_Tick: Failed to update chart data"); }
        }

        private void Timer_ClockTitle_Tick(object sender, EventArgs e)
        {
            this.Text = $"Stonks  {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
        }

        private void ToolStripComboBox_ChartValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ChartValues = toolStripComboBox_ChartValues.SelectedIndex;
            try { GetChartData(); } catch { Logger.LogError($"ToolStripMenuItem_ChartValues_SelectedIndexChanged: Failed to update chart data"); }
        }

        private void ToolStripMenuItem_About_Click(object sender, EventArgs e)
        {
            using (var _dlg = new AboutForm())
            {
                _dlg.ShowDialog();
            }
        }

        private void ToolStripMenuItem_Chart_Click(object sender, EventArgs e)
        {
            chart.Visible = !chart.Visible;
            Properties.Settings.Default.DisplayCharts = chart.Visible;
            toolStripComboBox_ChartValues.Enabled = chart.Visible;
            if (chart.Visible)
            {
                try { GetChartData(); } catch { Logger.LogError($"ToolStripMenuItem_Chart_Click: Failed to update chart data"); }
            }
        }

        private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                // If the app is running under the TrayApplicationContext, request a full exit
                TrayApplicationContext.Instance?.Exit();
            }
            catch
            {
                // fallback: close this form
                this.Close();
            }
        }

        private void ToolStripMenuItem_LogViewer_Click(object sender, EventArgs e)
        {
            using (var _dlg = new LogViewerForm())
            {
                _dlg.ShowDialog();
            }
        }

        private async void ToolStripMenuItem_RefreshTool_Click(object sender, EventArgs e)
        {
            try
            {
                await TrayApplicationContext.Instance.RefreshNow();
            }
            catch { Logger.LogError("MenuStripItemRefreshTool_Click: Failed to refresh data"); }
        }

        private void ToolStripMenuItem_Settings_Click(object sender, EventArgs e)
        {
            using (var _dlg = new SettingsForm())
            {
                _dlg.ShowDialog();
            }
        }

        private void GetChartData()
        {
            var _chartDataFilePath = Logger.ChartDataFile;
            if (!File.Exists(_chartDataFilePath)) return;

            // dictionary symbol -> list of (time, price)
            var map = new Dictionary<string, List<Tuple<DateTime, decimal>>>(StringComparer.OrdinalIgnoreCase);

            using (var _textFieldParser = new TextFieldParser(_chartDataFilePath) { TextFieldType = FieldType.Delimited, HasFieldsEnclosedInQuotes = true, TrimWhiteSpace = true })
            {
                _textFieldParser.SetDelimiters(",");
                // skip header if present
                if (!_textFieldParser.EndOfData)
                {
                    var _readFields = _textFieldParser.ReadFields();
                    // if header looks like Time,Symbol,Price,... then continue, else treat as data
                    if (_readFields.Length >= 4 &&
                        string.Equals(_readFields[0], "Time", StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(_readFields[1], "Symbol", StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(_readFields[2], "CurrentPrice", StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(_readFields[3], "PercentChange", StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(_readFields[4], "Shares", StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(_readFields[5], "Value", StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(_readFields[6], "PricePaid", StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(_readFields[7], "Gain", StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(_readFields[8], "GainPercent", StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(_readFields[9], "FiftyTwoWeekHigh", StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(_readFields[10], "FiftyTwoWeekLow", StringComparison.OrdinalIgnoreCase)
                        )
                    {
                        // header consumed
                        // "Time,Symbol,CurrentPrice,PercentChange,Shares,Value,PricePaid,Gain,GainPercent,FiftyTwoWeekHigh,FiftyTwoWeekLow"
                    }
                    else
                    {
                        // first line was data
                        ProcessFields(_readFields, map);
                    }
                }

                while (!_textFieldParser.EndOfData)
                {
                    var _fields = _textFieldParser.ReadFields();
                    ProcessFields(_fields, map);
                }
            }

            // populate chart
            // Only chart symbols that have the "Chart" checkbox enabled in the stock list
            chart.Series.Clear();
            var _hashSet_Allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            try
            {
                foreach (DataGridViewRow _dgv_Row in dataGridView_StockList.Rows)
                {
                    try
                    {
                        var _cellVal = _dgv_Row.Cells.Count > 0 ? _dgv_Row.Cells["colChart"].Value : null;
                        bool _enabled = false;
                        if (_cellVal is bool b) _enabled = b;
                        else if (_cellVal != null) bool.TryParse(_cellVal.ToString(), out _enabled);
                        var _symbol = _dgv_Row.Cells.Count > 1 ? _dgv_Row.Cells["colSymbol"].Value?.ToString() : null;
                        if (_enabled && !string.IsNullOrWhiteSpace(_symbol)) _hashSet_Allowed.Add(_symbol);
                    }
                    catch { Logger.LogError("GetChartData: Failed to process dataGridView_StockList rows"); }
                }
            }
            catch { Logger.LogError("GetChartData: Failed to process stock data from dataGridView_StockList"); }

            foreach (var kv in map.Where(kv => _hashSet_Allowed.Contains(kv.Key)).OrderBy(k => k.Key))
            {
                var _symbol = kv.Key;
                var _points = kv.Value.OrderBy(p => p.Item1).ToList();
                var _series = new Series(_symbol)
                {
                    ChartType = SeriesChartType.Line,
                    XValueType = ChartValueType.DateTime,
                    BorderWidth = 2,
                    MarkerStyle = MarkerStyle.Circle,
                };
                switch (toolStripComboBox_ChartValues.SelectedIndex)
                {
                    case 0:
                        _series.ChartType = SeriesChartType.Line;
                        _series.ToolTip = $"{_symbol}: #VALY{{C}}\\n#VALX{{HH:mm}}";
                        break;
                    case 1: // Percent Change
                    case 4: // Percent Gain
                        _series.ChartType = SeriesChartType.Line;
                        _series.ToolTip = $"{_symbol}: #VALY{{0.0\\%}}\\n#VALX{{HH:mm}}";
                        break;
                    case 2: // Value
                    case 3: // Gain
                        _series.ChartType = SeriesChartType.StackedArea;
                        _series.ToolTip = $"{_symbol}: #VALY{{C}}\\n#VALX{{HH:mm}}";
                        break;
                    default:
                        _series.ChartType = SeriesChartType.Line;
                        _series.ToolTip = $"{_symbol}: #VALY{{C}}\\n#VALX{{HH:mm}}";
                        break;
                }

                foreach (var _point in _points)
                {
                    _series.Points.AddXY(_point.Item1, _point.Item2);
                }
                chart.Series.Add(_series);
            }

            //reset chartarea's y axis
            chart.ChartAreas[0] = new ChartArea("chartArea1");
            if (chart.ChartAreas.Count > 0)
            {
                var _chartArea = chart.ChartAreas[0];
                _chartArea.AxisX.IsMarginVisible = false;
                _chartArea.AxisX.LabelStyle.Angle = -45;
                _chartArea.AxisX.LabelStyle.Format = "HH:mm";
                _chartArea.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                _chartArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

                switch (toolStripComboBox_ChartValues.SelectedIndex)
                {
                    case 1: // Percent Change
                    case 4: // Gain Percent
                        _chartArea.AxisY.LabelStyle.Format = "0\\%";
                        _chartArea.AxisY.Title = "Percent";
                        break;
                    default:
                        _chartArea.AxisY.LabelStyle.Format = "$#,##0";
                        _chartArea.AxisY.Title = "Dollars";
                        break;
                }
            }
            else
            {
                Logger.LogError($"ChartArea[\"chartArea1\"] not found");
                return;
            }

            // local function to parse and insert
            void ProcessFields(string[] fields, Dictionary<string, List<Tuple<DateTime, decimal>>> dict)
            {
                try
                {
                    if (fields == null || fields.Length < 4) return;
                    var _dateText = fields[0].Trim(' ', '"');
                    var _symbol = fields[1].Trim(' ', '"');
                    var _currentPriceText = fields[2].Trim(' ', '"');
                    var _percentChangeText = fields[3].Trim(' ', '"');
                    var _sharesText = fields[4].Trim(' ', '"');
                    var _valueText = fields[5].Trim(' ', '"');
                    var _pricePaidText = fields[6].Trim(' ', '"');
                    var _gainText = fields[7].Trim(' ', '"');
                    var _percentGainText = fields[8].Trim(' ', '"');
                    var _fiftyTwoWeekHighText = fields[9].Trim(' ', '"');
                    var _fiftyTwoWeekLowText = fields[10].Trim(' ', '"');

                    if (!DateTime.TryParseExact(_dateText, new[] { "yyyy-MM-dd HH:mm", "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd H:mm" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt)) { Logger.LogError($"Invalid date for row: {string.Join(",", fields)}"); return; }
                    if (string.IsNullOrWhiteSpace(_symbol)) { Logger.LogError($"Invalid symbol for row: {string.Join(",", fields)}"); return; }
                    if (!decimal.TryParse(_currentPriceText, NumberStyles.Any, CultureInfo.InvariantCulture, out var currentPrice)) { Logger.LogError($"Invalid current price for row: {string.Join(",", fields)}"); return; }
                    if (!decimal.TryParse(_percentChangeText, NumberStyles.Any, CultureInfo.InvariantCulture, out var percentChange)) { Logger.LogError($"Invalid percent change for row: {string.Join(",", fields)}"); return; }
                    if (!decimal.TryParse(_sharesText, NumberStyles.Any, CultureInfo.InvariantCulture, out var shares)) { Logger.LogError($"Invalid shares for row: {string.Join(",", fields)}"); return; }
                    if (!decimal.TryParse(_valueText, NumberStyles.Any, CultureInfo.InvariantCulture, out var value)) { Logger.LogError($"Invalid value for row: {string.Join(",", fields)}"); return; }
                    if (!decimal.TryParse(_pricePaidText, NumberStyles.Any, CultureInfo.InvariantCulture, out var pricePaid)) { Logger.LogError($"Invalid price paid for row: {string.Join(",", fields)}"); return; }
                    if (!decimal.TryParse(_gainText, NumberStyles.Any, CultureInfo.InvariantCulture, out var gain)) { Logger.LogError($"Invalid gain for row: {string.Join(",", fields)}"); return; }
                    if (!decimal.TryParse(_percentGainText, NumberStyles.Any, CultureInfo.InvariantCulture, out var percentGain)) { Logger.LogError($"Invalid percent gain for row: {string.Join(",", fields)}"); return; }
                    if (!decimal.TryParse(_fiftyTwoWeekHighText, NumberStyles.Any, CultureInfo.InvariantCulture, out var fiftyTwoWeekHigh)) { Logger.LogError($"Invalid 52-week high for row: {string.Join(",", fields)}"); return; }
                    if (!decimal.TryParse(_fiftyTwoWeekLowText, NumberStyles.Any, CultureInfo.InvariantCulture, out var fiftyTwoWeekLow)) { Logger.LogError($"Invalid 52-week low for row: {string.Join(",", fields)}"); return; }

                    if (!dict.TryGetValue(_symbol, out var _pointsList))
                    {
                        _pointsList = new List<Tuple<DateTime, decimal>>();
                        dict[_symbol] = _pointsList;
                    }
                    switch (toolStripComboBox_ChartValues.SelectedIndex)
                    {
                        case 0: // Price
                            _pointsList.Add(Tuple.Create(dt, currentPrice));
                            break;
                        case 1: // Percent Change
                            _pointsList.Add(Tuple.Create(dt, percentChange));
                            break;
                        case 2: // Value
                            _pointsList.Add(Tuple.Create(dt, value));
                            break;
                        case 3: // Gain
                            _pointsList.Add(Tuple.Create(dt, gain));
                            break;
                        case 4: // Gain Percent
                            _pointsList.Add(Tuple.Create(dt, percentGain));
                            break;
                    }
                }
                catch { Logger.LogError($"ProcessFields: Error occurred while processing row: {string.Join(",", fields)}"); }
            }
        }

        public void UpdateStockList(List<StockViewModel> stocks)
        {
            Logger.LogDebug("UpdateStockList: called with count=" + (stocks?.Count ?? 0));
            dataGridView_StockList.Rows.Clear();
            decimal _sumValue = 0;
            decimal _sumGain = 0;
            decimal _sumPaid = 0;
            var _time = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            foreach (var _stock in stocks)
            {
                var row = dataGridView_StockList.Rows.Add(
                    _stock.Chart, // checkbox for "include in chart"
                    _stock.Symbol ?? string.Empty,
                    _stock.Name ?? string.Empty,
                    _stock.CurrentPrice.ToString("N2"),
                    _stock.PercentChange.ToString("+0.00;-0.00;0.00") + "%",
                    _stock.Shares.ToString("N2"),
                    _stock.Value.ToString("N2"),
                    _stock.PricePaid.ToString("N2"),
                    _stock.Gain.ToString("N2"),
                    _stock.GainPercent.ToString("+0.00;-0.00;0.00") + "%",
                    _stock.FiftyTwoWeekHigh.ToString("N2"),
                    _stock.FiftyTwoWeekLow.ToString("N2"),
                    _stock.Notes ?? string.Empty);

                Logger.LogChartData(_time, _stock.Symbol, _stock.CurrentPrice, _stock.PercentChange, _stock.Shares, _stock.Value, _stock.PricePaid, _stock.Gain, _stock.GainPercent, _stock.FiftyTwoWeekHigh, _stock.FiftyTwoWeekLow);

                dataGridView_StockList.Rows[row].Tag = _stock;
                _sumValue += _stock.CurrentPrice * _stock.Shares;
                _sumGain += _stock.Gain;
                _sumPaid += _stock.PricePaid * _stock.Shares;

             

                toolStripStatusLabel_Value.Text = $"Total Value: {_sumValue:N2}";
                toolStripStatusLabel_Gain.Text = $"Total Gain: {_sumGain:N2}";
                toolStripStatusLabel_Paid.Text = $"Total Paid: {_sumPaid:N2}";
            }
            dataGridView_StockList.ClearSelection();
        }

        private void chart_MouseMove(object sender, MouseEventArgs e)
        {
            var _hitTest = chart.HitTest(e.X, e.Y);
            if (_hitTest != null && _hitTest.ChartElementType == ChartElementType.LegendItem && _hitTest.Series != null)
            {
                var _selectedSeries = _hitTest.Series;
                if (_highlightedSeries != _selectedSeries)
                {
                    Restore();
                    Highlight(_selectedSeries);
                }
            }
            else
            {
                Restore();
            }
        }

        private void chart_MouseLeave(object sender, EventArgs e)
        {
            Restore();
        }
        private void Highlight(Series selectedSeries)
        {
            _highlightedSeries = selectedSeries;
            foreach (var series in chart.Series)
            {
                // modify chart
                if (!_origBorderWidth.ContainsKey(series))
                {
                    _origBorderWidth[series] = series.BorderWidth;
                    _origMarkerSize[series] = series.MarkerSize;
                    if (series.ChartType == SeriesChartType.StackedArea)
                    {
                        series.BackHatchStyle = ChartHatchStyle.LightUpwardDiagonal; // use hatch to visually highlight area charts, since changing color also changes border color which looks bad
                    }
                }

                if (series == selectedSeries)
                {
                    if (series.ChartType == SeriesChartType.Line) // for line charts, increase marker size and border width to make it stand out more
                    {
                        series.MarkerSize = Math.Max(5, series.MarkerSize + 2);
                        series.BorderWidth = Math.Max(3, series.BorderWidth + 2);
                    }
                    var _row = dataGridView_StockList.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => r.Cells["colSymbol"].Value?.ToString() == series.Name);
                    if (_row != null)
                    {
                        _row.DefaultCellStyle.BackColor = SystemColors.Highlight; // highlight matching row in stock list
                        _row.DefaultCellStyle.ForeColor = SystemColors.HighlightText;
                    }
                }
                else
                {
                    var _borderWidth = series.BorderWidth;
                    var _markerSize = series.MarkerSize;
                    if (series.ChartType == SeriesChartType.StackedArea)
                    {
                        series.BackHatchStyle = ChartHatchStyle.None; // remove hatch style to ensure faded color is visible
                    }
                    var _row = dataGridView_StockList.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => r.Cells["colSymbol"].Value?.ToString() == series.Name);
                    if (_row != null)
                    {
                        _row.DefaultCellStyle.BackColor = Color.Empty;
                        _row.DefaultCellStyle.ForeColor = Color.Empty;
                    }

                }
            }
        }

        private void Restore()
        {
            if (_highlightedSeries == null) return;
            foreach (var series in chart.Series)
            {
                if (_origBorderWidth.TryGetValue(series, out var obw)) series.BorderWidth = obw;
                if (_origMarkerSize.TryGetValue(series, out var oms)) series.MarkerSize = oms;
                if (series.ChartType == SeriesChartType.StackedArea)
                {
                    series.BackHatchStyle = ChartHatchStyle.None;
                }
            }
            foreach(DataGridViewRow _row in dataGridView_StockList.Rows)
            {
                _row.DefaultCellStyle.BackColor = Color.Empty;
                _row.DefaultCellStyle.ForeColor = Color.Empty;
            }            
            _origBorderWidth.Clear();
            _origMarkerSize.Clear();
            _highlightedSeries = null;
        }
    }
}
