using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Stonks
{
    public partial class SettingsForm : Form
    {

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void ShowApi_CheckedChanged(object sender, EventArgs e)
        {
            _boxApiKey.UseSystemPasswordChar = !_cbShowApi.Checked;
            Logger.LogInfo($"SettingsForm: ShowApi_CheckedChanged: {_cbShowApi.Checked}");
        }


        private void Save_Click(object sender, EventArgs e)
        {
            var stockList = new List<StockDefinition>();

            if (!string.IsNullOrEmpty(_boxApiKey.Text))
            {
                var crypto = new Crypto() { ApiKey = _boxApiKey.Text.Trim() };
                Properties.Settings.Default.EncryptedApiKey = crypto.EncryptedApiKey;
            }
            else
            {
                Properties.Settings.Default.EncryptedApiKey = null;
            }

            foreach (DataGridViewRow row in _datagridStockList.Rows)
            {
                if (row.IsNewRow) continue;
                var sym = (row.Cells[0].Value ?? string.Empty).ToString().Trim();
                if (string.IsNullOrEmpty(sym)) continue;
                var sharesText = (row.Cells[1].Value ?? "0").ToString();
                var priceText = (row.Cells[2].Value ?? "0").ToString();
                var chartText = (row.Cells[4].Value ?? false).ToString();
                var notes = (row.Cells[3].Value ?? string.Empty).ToString();

                if (!decimal.TryParse(sharesText, NumberStyles.Number | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out var shares)) shares = 0m;
                if (!decimal.TryParse(priceText, NumberStyles.Number, CultureInfo.CurrentCulture, out var price)) price = 0m;
                if (!bool.TryParse(chartText, out var chart)) chart = false;
                stockList.Add(new StockDefinition { Symbol = sym, Shares = shares, PricePaid = price, Notes = notes, Chart = chart });
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<StockDefinition>));

            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, stockList, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
                Properties.Settings.Default.StockDefinitions = writer.ToString();
            }


            Properties.Settings.Default.Save();
            Logger.LogInfo($"SettingsForm: Settings Saved");
            //SaveSettings();
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Logger.LogInfo($"SettingsForm: Settings Cancelled");
            this.Close();
        }

        private void BrowseLogDir_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dlg = new FolderBrowserDialog())
                {
                    dlg.Description = "Select log folder";
                    dlg.SelectedPath = _boxLogDir.Text;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        _boxLogDir.Text = dlg.SelectedPath;
                    }
                }
            }
            catch { }
        }

        // placeholder in case runtime needs grid mouse handling (designer-safe)
        private void Grid_MouseMove(object sender, MouseEventArgs e) { }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

            _datagridStockList.Rows.Clear();
            var crypto = new Crypto() { EncryptedApiKey = Properties.Settings.Default.EncryptedApiKey };
            _boxApiKey.Text = crypto.ApiKey;
            if (string.IsNullOrWhiteSpace(_boxApiKey.Text))
            {
                Logger.LogError($"ApiKey not defined");
                _boxApiKey.BackColor = System.Drawing.Color.LightPink;
            }
            else
            {
                _boxApiKey.BackColor = System.Drawing.SystemColors.Window;
            }

            var stockList = new List<StockDefinition>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<StockDefinition>));
            using (var reader = new StringReader(Properties.Settings.Default.StockDefinitions))
            {
                stockList = (List<StockDefinition>)serializer.Deserialize(reader);
            }



            foreach (var sd in stockList ?? new List<StockDefinition>())
            {
                if (!bool.TryParse(sd.Chart.ToString(), out var chart)) chart = false;
                _datagridStockList.Rows.Add(
                    sd.Symbol ?? string.Empty,
                    (sd.Shares).ToString("0.00##", CultureInfo.CurrentCulture),
                    (sd.PricePaid).ToString("0.00##", CultureInfo.CurrentCulture),
                    sd.Notes ?? string.Empty,
                    chart);
            }
            Logger.LogInfo($"SettingsForm: Settings Loaded");
        }

        private void _apiKeyBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                Logger.LogError($"ApiKey not defined");
                _boxApiKey.BackColor = System.Drawing.Color.LightPink;
            }
            else
            {
                _boxApiKey.BackColor = System.Drawing.SystemColors.Window;
            }
        }
    }
}
