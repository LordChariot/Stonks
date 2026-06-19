using System;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

namespace Stonks
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        // Designer-managed controls
        private TextBox _boxApiKey;
        private CheckBox _cbShowApi;
        private NumericUpDown _nudInterval;
        private CheckBox _cbVerbose;
        private TextBox _boxLogDir;
        private Button _browseLogDir;
        private Button _btnSave;
        private Button _btnCancel;
        private DataGridView _datagridStockList;



        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this._boxApiKey = new System.Windows.Forms.TextBox();
            this._datagridStockList = new System.Windows.Forms.DataGridView();
            this._colSymbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colShares = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colPricePaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colChart = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this._btnSave = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this._lblApi = new System.Windows.Forms.Label();
            this._lblInterval = new System.Windows.Forms.Label();
            this._browseLogDir = new System.Windows.Forms.Button();
            this._lblLogDir = new System.Windows.Forms.Label();
            this._cbShowApi = new System.Windows.Forms.CheckBox();
            this._browseChartDir = new System.Windows.Forms.Button();
            this._cbShowCharts = new System.Windows.Forms.CheckBox();
            this._nudInterval = new System.Windows.Forms.NumericUpDown();
            this._cbVerbose = new System.Windows.Forms.CheckBox();
            this._boxLogDir = new System.Windows.Forms.TextBox();
            this._boxChartDir = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this._datagridStockList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // _boxApiKey
            // 
            this._boxApiKey.Location = new System.Drawing.Point(90, 12);
            this._boxApiKey.Name = "_boxApiKey";
            this._boxApiKey.Size = new System.Drawing.Size(412, 27);
            this._boxApiKey.TabIndex = 0;
            this._boxApiKey.UseSystemPasswordChar = true;
            this._boxApiKey.TextChanged += new System.EventHandler(this._apiKeyBox_TextChanged);
            // 
            // _datagridStockList
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._datagridStockList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this._datagridStockList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._datagridStockList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this._datagridStockList.ColumnHeadersHeight = 28;
            this._datagridStockList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._colSymbol,
            this._colShares,
            this._colPricePaid,
            this._colNotes,
            this._colChart});
            this._datagridStockList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this._datagridStockList.Location = new System.Drawing.Point(12, 144);
            this._datagridStockList.Name = "_datagridStockList";
            this._datagridStockList.RowHeadersVisible = false;
            this._datagridStockList.Size = new System.Drawing.Size(560, 214);
            this._datagridStockList.TabIndex = 0;
            this._datagridStockList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseMove);
            // 
            // _colSymbol
            // 
            this._colSymbol.HeaderText = "Symbol";
            this._colSymbol.Name = "_colSymbol";
            // 
            // _colShares
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this._colShares.DefaultCellStyle = dataGridViewCellStyle3;
            this._colShares.HeaderText = "Shares";
            this._colShares.Name = "_colShares";
            // 
            // _colPricePaid
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            this._colPricePaid.DefaultCellStyle = dataGridViewCellStyle4;
            this._colPricePaid.HeaderText = "Price Paid";
            this._colPricePaid.Name = "_colPricePaid";
            // 
            // _colNotes
            // 
            this._colNotes.HeaderText = "Notes";
            this._colNotes.Name = "_colNotes";
            // 
            // _colChart
            // 
            this._colChart.HeaderText = "Chart";
            this._colChart.Name = "_colChart";
            // 
            // _btnSave
            // 
            this._btnSave.Location = new System.Drawing.Point(406, 364);
            this._btnSave.Name = "_btnSave";
            this._btnSave.Size = new System.Drawing.Size(80, 32);
            this._btnSave.TabIndex = 0;
            this._btnSave.Text = "Save";
            this._btnSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // _btnCancel
            // 
            this._btnCancel.Location = new System.Drawing.Point(492, 364);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(80, 32);
            this._btnCancel.TabIndex = 0;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // _lblApi
            // 
            this._lblApi.AutoSize = true;
            this._lblApi.Location = new System.Drawing.Point(22, 16);
            this._lblApi.Name = "_lblApi";
            this._lblApi.Size = new System.Drawing.Size(62, 20);
            this._lblApi.TabIndex = 0;
            this._lblApi.Text = "API Key:";
            // 
            // _lblInterval
            // 
            this._lblInterval.AutoSize = true;
            this._lblInterval.Location = new System.Drawing.Point(23, 47);
            this._lblInterval.Name = "_lblInterval";
            this._lblInterval.Size = new System.Drawing.Size(61, 20);
            this._lblInterval.TabIndex = 0;
            this._lblInterval.Text = "Interval:";
            // 
            // _browseLogDir
            // 
            this._browseLogDir.Location = new System.Drawing.Point(522, 73);
            this._browseLogDir.Name = "_browseLogDir";
            this._browseLogDir.Size = new System.Drawing.Size(50, 30);
            this._browseLogDir.TabIndex = 0;
            this._browseLogDir.Text = "...";
            this._browseLogDir.Click += new System.EventHandler(this.BrowseLogDir_Click);
            // 
            // _lblLogDir
            // 
            this._lblLogDir.AutoSize = true;
            this._lblLogDir.Location = new System.Drawing.Point(41, 83);
            this._lblLogDir.Name = "_lblLogDir";
            this._lblLogDir.Size = new System.Drawing.Size(43, 20);
            this._lblLogDir.TabIndex = 0;
            this._lblLogDir.Text = "Logs:";
            // 
            // _cbShowApi
            // 
            this._cbShowApi.AutoSize = true;
            this._cbShowApi.Location = new System.Drawing.Point(508, 15);
            this._cbShowApi.Name = "_cbShowApi";
            this._cbShowApi.Size = new System.Drawing.Size(64, 24);
            this._cbShowApi.TabIndex = 0;
            this._cbShowApi.Text = "Show";
            this._cbShowApi.CheckedChanged += new System.EventHandler(this.ShowApi_CheckedChanged);
            // 
            // _browseChartDir
            // 
            this._browseChartDir.Location = new System.Drawing.Point(522, 109);
            this._browseChartDir.Name = "_browseChartDir";
            this._browseChartDir.Size = new System.Drawing.Size(50, 30);
            this._browseChartDir.TabIndex = 0;
            this._browseChartDir.Text = "...";
            // 
            // _cbShowCharts
            // 
            this._cbShowCharts.AutoSize = true;
            this._cbShowCharts.Checked = global::Stonks.Properties.Settings.Default.DisplayCharts;
            this._cbShowCharts.CheckState = System.Windows.Forms.CheckState.Checked;
            this._cbShowCharts.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Stonks.Properties.Settings.Default, "DisplayCharts", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this._cbShowCharts.Location = new System.Drawing.Point(12, 113);
            this._cbShowCharts.Name = "_cbShowCharts";
            this._cbShowCharts.Size = new System.Drawing.Size(72, 24);
            this._cbShowCharts.TabIndex = 0;
            this._cbShowCharts.Text = "Charts:";
            this._cbShowCharts.UseVisualStyleBackColor = true;
            // 
            // _nudInterval
            // 
            this._nudInterval.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Stonks.Properties.Settings.Default, "PollingInterval", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this._nudInterval.Location = new System.Drawing.Point(90, 45);
            this._nudInterval.Maximum = new decimal(new int[] {
            86400,
            0,
            0,
            0});
            this._nudInterval.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._nudInterval.Name = "_nudInterval";
            this._nudInterval.Size = new System.Drawing.Size(90, 27);
            this._nudInterval.TabIndex = 0;
            this._nudInterval.Value = global::Stonks.Properties.Settings.Default.PollingInterval;
            // 
            // _cbVerbose
            // 
            this._cbVerbose.AutoSize = true;
            this._cbVerbose.Checked = global::Stonks.Properties.Settings.Default.VerboseLogging;
            this._cbVerbose.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Stonks.Properties.Settings.Default, "VerboseLogging", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this._cbVerbose.Location = new System.Drawing.Point(186, 46);
            this._cbVerbose.Name = "_cbVerbose";
            this._cbVerbose.Size = new System.Drawing.Size(140, 24);
            this._cbVerbose.TabIndex = 0;
            this._cbVerbose.Text = "Verbose Logging";
            // 
            // _boxLogDir
            // 
            this._boxLogDir.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Stonks.Properties.Settings.Default, "LogDirectory", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this._boxLogDir.Location = new System.Drawing.Point(90, 78);
            this._boxLogDir.Name = "_boxLogDir";
            this._boxLogDir.Size = new System.Drawing.Size(426, 27);
            this._boxLogDir.TabIndex = 0;
            this._boxLogDir.Text = global::Stonks.Properties.Settings.Default.LogDirectory;
            // 
            // _boxChartDir
            // 
            this._boxChartDir.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Stonks.Properties.Settings.Default, "LogDirectory", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this._boxChartDir.Location = new System.Drawing.Point(90, 111);
            this._boxChartDir.Name = "_boxChartDir";
            this._boxChartDir.Size = new System.Drawing.Size(426, 27);
            this._boxChartDir.TabIndex = 0;
            this._boxChartDir.Text = global::Stonks.Properties.Settings.Default.LogDirectory;
            // 
            // SettingsForm
            // 
            this.ClientSize = new System.Drawing.Size(584, 406);
            this.Controls.Add(this._lblApi);
            this.Controls.Add(this._boxApiKey);
            this.Controls.Add(this._cbShowApi);
            this.Controls.Add(this._lblInterval);
            this.Controls.Add(this._nudInterval);
            this.Controls.Add(this._cbVerbose);
            this.Controls.Add(this._lblLogDir);
            this.Controls.Add(this._boxLogDir);
            this.Controls.Add(this._browseLogDir);
            this.Controls.Add(this._cbShowCharts);
            this.Controls.Add(this._boxChartDir);
            this.Controls.Add(this._browseChartDir);
            this.Controls.Add(this._datagridStockList);
            this.Controls.Add(this._btnSave);
            this.Controls.Add(this._btnCancel);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._datagridStockList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private Label _lblApi;
        private Label _lblInterval;
        private Label _lblLogDir;
        private TextBox _boxChartDir;
        private Button _browseChartDir;
        private CheckBox _cbShowCharts;
        private DataGridViewTextBoxColumn _colSymbol;
        private DataGridViewTextBoxColumn _colShares;
        private DataGridViewTextBoxColumn _colPricePaid;
        private DataGridViewTextBoxColumn _colNotes;
        private DataGridViewCheckBoxColumn _colChart;
    }
}
