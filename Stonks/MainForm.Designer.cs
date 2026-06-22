using System;
using System.Windows.Forms;
using System.Drawing;

namespace Stonks
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private ToolTip tooltip;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip_1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_Value = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_Paid = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_Gain = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridView_StockList = new System.Windows.Forms.DataGridView();
            this.colChart = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSymbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCurrent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShares = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGainPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col52High = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col52Low = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip_1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_LogViewer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator_1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Chart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox_ChartValues = new System.Windows.Forms.ToolStripComboBox();
            this.timer_ClockTitle = new System.Windows.Forms.Timer(this.components);
            this.timer_Chart = new System.Windows.Forms.Timer(this.components);
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolStripStatusLabel_AvgPercentChange = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip_1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_StockList)).BeginInit();
            this.menuStrip_1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // tooltip
            // 
            this.tooltip.AutomaticDelay = 100;
            this.tooltip.ShowAlways = true;
            // 
            // statusStrip_1
            // 
            this.statusStrip_1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip_1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_Value,
            this.toolStripStatusLabel_AvgPercentChange,
            this.toolStripStatusLabel_Paid,
            this.toolStripStatusLabel_Gain});
            this.statusStrip_1.Location = new System.Drawing.Point(0, 646);
            this.statusStrip_1.Name = "statusStrip_1";
            this.statusStrip_1.Size = new System.Drawing.Size(944, 29);
            this.statusStrip_1.TabIndex = 0;
            this.statusStrip_1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_Value
            // 
            this.toolStripStatusLabel_Value.AutoSize = false;
            this.toolStripStatusLabel_Value.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel_Value.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.toolStripStatusLabel_Value.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel_Value.Name = "toolStripStatusLabel_Value";
            this.toolStripStatusLabel_Value.Size = new System.Drawing.Size(191, 24);
            this.toolStripStatusLabel_Value.Text = "Total Value: 0,000,000.00";
            this.toolStripStatusLabel_Value.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel_Paid
            // 
            this.toolStripStatusLabel_Paid.AutoSize = false;
            this.toolStripStatusLabel_Paid.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel_Paid.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.toolStripStatusLabel_Paid.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel_Paid.Name = "toolStripStatusLabel_Paid";
            this.toolStripStatusLabel_Paid.Size = new System.Drawing.Size(183, 24);
            this.toolStripStatusLabel_Paid.Text = "Total Paid: 0,000,000.00";
            // 
            // toolStripStatusLabel_Gain
            // 
            this.toolStripStatusLabel_Gain.AutoSize = false;
            this.toolStripStatusLabel_Gain.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel_Gain.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.toolStripStatusLabel_Gain.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel_Gain.Name = "toolStripStatusLabel_Gain";
            this.toolStripStatusLabel_Gain.Size = new System.Drawing.Size(185, 24);
            this.toolStripStatusLabel_Gain.Text = "Total Gain: 0,000,000.00";
            this.toolStripStatusLabel_Gain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dataGridView_StockList
            // 
            this.dataGridView_StockList.AllowUserToAddRows = false;
            this.dataGridView_StockList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView_StockList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_StockList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_StockList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_StockList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_StockList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChart,
            this.colSymbol,
            this.colName,
            this.colCurrent,
            this.colPercent,
            this.colShares,
            this.colValue,
            this.colPaid,
            this.colGain,
            this.colGainPercent,
            this.col52High,
            this.col52Low,
            this.colNotes});
            this.dataGridView_StockList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_StockList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView_StockList.Location = new System.Drawing.Point(0, 327);
            this.dataGridView_StockList.Name = "dataGridView_StockList";
            this.dataGridView_StockList.RowHeadersVisible = false;
            this.dataGridView_StockList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_StockList.Size = new System.Drawing.Size(944, 319);
            this.dataGridView_StockList.TabIndex = 0;
            // 
            // colChart
            // 
            this.colChart.HeaderText = "Chart";
            this.colChart.Name = "colChart";
            this.colChart.Width = 50;
            // 
            // colSymbol
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSymbol.DefaultCellStyle = dataGridViewCellStyle3;
            this.colSymbol.HeaderText = "Symbol";
            this.colSymbol.Name = "colSymbol";
            this.colSymbol.ReadOnly = true;
            this.colSymbol.Width = 84;
            // 
            // colName
            // 
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 74;
            // 
            // colCurrent
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colCurrent.DefaultCellStyle = dataGridViewCellStyle4;
            this.colCurrent.HeaderText = "Current";
            this.colCurrent.Name = "colCurrent";
            this.colCurrent.ReadOnly = true;
            this.colCurrent.Width = 82;
            // 
            // colPercent
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colPercent.DefaultCellStyle = dataGridViewCellStyle5;
            this.colPercent.HeaderText = "%Change";
            this.colPercent.Name = "colPercent";
            this.colPercent.ReadOnly = true;
            this.colPercent.Width = 96;
            // 
            // colShares
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colShares.DefaultCellStyle = dataGridViewCellStyle6;
            this.colShares.HeaderText = "Shares";
            this.colShares.Name = "colShares";
            this.colShares.ReadOnly = true;
            this.colShares.Width = 77;
            // 
            // colValue
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colValue.DefaultCellStyle = dataGridViewCellStyle7;
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            this.colValue.ReadOnly = true;
            this.colValue.Width = 70;
            // 
            // colPaid
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colPaid.DefaultCellStyle = dataGridViewCellStyle8;
            this.colPaid.HeaderText = "Paid";
            this.colPaid.Name = "colPaid";
            this.colPaid.ReadOnly = true;
            this.colPaid.Width = 62;
            // 
            // colGain
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colGain.DefaultCellStyle = dataGridViewCellStyle9;
            this.colGain.HeaderText = "Gain";
            this.colGain.Name = "colGain";
            this.colGain.ReadOnly = true;
            this.colGain.Width = 64;
            // 
            // colGainPercent
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colGainPercent.DefaultCellStyle = dataGridViewCellStyle10;
            this.colGainPercent.HeaderText = "%Gain";
            this.colGainPercent.Name = "colGainPercent";
            this.colGainPercent.ReadOnly = true;
            this.colGainPercent.Width = 76;
            // 
            // col52High
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col52High.DefaultCellStyle = dataGridViewCellStyle11;
            this.col52High.HeaderText = "52-High";
            this.col52High.Name = "col52High";
            this.col52High.ReadOnly = true;
            this.col52High.Width = 88;
            // 
            // col52Low
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col52Low.DefaultCellStyle = dataGridViewCellStyle12;
            this.col52Low.HeaderText = "52-Low";
            this.col52Low.Name = "col52Low";
            this.col52Low.ReadOnly = true;
            this.col52Low.Width = 83;
            // 
            // colNotes
            // 
            this.colNotes.HeaderText = "Notes";
            this.colNotes.Name = "colNotes";
            this.colNotes.ReadOnly = true;
            this.colNotes.Width = 73;
            // 
            // menuStrip_1
            // 
            this.menuStrip_1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_File,
            this.toolStripMenuItem_Refresh,
            this.toolStripMenuItem_Chart,
            this.toolStripComboBox_ChartValues});
            this.menuStrip_1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_1.Name = "menuStrip_1";
            this.menuStrip_1.Size = new System.Drawing.Size(944, 27);
            this.menuStrip_1.TabIndex = 1;
            this.menuStrip_1.Text = "menuStrip1";
            // 
            // toolStripMenuItem_File
            // 
            this.toolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Settings,
            this.toolStripMenuItem_LogViewer,
            this.toolStripMenuItem_About,
            this.toolStripSeparator_1,
            this.toolStripMenuItem_Exit});
            this.toolStripMenuItem_File.Image = global::Stonks.Properties.Resources.WindowsFormToolBox;
            this.toolStripMenuItem_File.Name = "toolStripMenuItem_File";
            this.toolStripMenuItem_File.Size = new System.Drawing.Size(73, 23);
            this.toolStripMenuItem_File.Text = "System";
            // 
            // toolStripMenuItem_Settings
            // 
            this.toolStripMenuItem_Settings.Image = global::Stonks.Properties.Resources.Settings;
            this.toolStripMenuItem_Settings.Name = "toolStripMenuItem_Settings";
            this.toolStripMenuItem_Settings.Size = new System.Drawing.Size(132, 22);
            this.toolStripMenuItem_Settings.Text = "Settings";
            this.toolStripMenuItem_Settings.Click += new System.EventHandler(this.ToolStripMenuItem_Settings_Click);
            // 
            // toolStripMenuItem_LogViewer
            // 
            this.toolStripMenuItem_LogViewer.Image = global::Stonks.Properties.Resources.Log;
            this.toolStripMenuItem_LogViewer.Name = "toolStripMenuItem_LogViewer";
            this.toolStripMenuItem_LogViewer.Size = new System.Drawing.Size(132, 22);
            this.toolStripMenuItem_LogViewer.Text = "Log Viewer";
            this.toolStripMenuItem_LogViewer.Click += new System.EventHandler(this.ToolStripMenuItem_LogViewer_Click);
            // 
            // toolStripMenuItem_About
            // 
            this.toolStripMenuItem_About.Image = global::Stonks.Properties.Resources.AboutBox;
            this.toolStripMenuItem_About.Name = "toolStripMenuItem_About";
            this.toolStripMenuItem_About.Size = new System.Drawing.Size(132, 22);
            this.toolStripMenuItem_About.Text = "About";
            this.toolStripMenuItem_About.Click += new System.EventHandler(this.ToolStripMenuItem_About_Click);
            // 
            // toolStripSeparator_1
            // 
            this.toolStripSeparator_1.Name = "toolStripSeparator_1";
            this.toolStripSeparator_1.Size = new System.Drawing.Size(129, 6);
            // 
            // toolStripMenuItem_Exit
            // 
            this.toolStripMenuItem_Exit.Image = global::Stonks.Properties.Resources.Exit;
            this.toolStripMenuItem_Exit.Name = "toolStripMenuItem_Exit";
            this.toolStripMenuItem_Exit.Size = new System.Drawing.Size(132, 22);
            this.toolStripMenuItem_Exit.Text = "Exit";
            this.toolStripMenuItem_Exit.Click += new System.EventHandler(this.ToolStripMenuItem_Exit_Click);
            // 
            // toolStripMenuItem_Refresh
            // 
            this.toolStripMenuItem_Refresh.Image = global::Stonks.Properties.Resources.Refresh;
            this.toolStripMenuItem_Refresh.Name = "toolStripMenuItem_Refresh";
            this.toolStripMenuItem_Refresh.Size = new System.Drawing.Size(102, 23);
            this.toolStripMenuItem_Refresh.Text = "Refresh Now";
            this.toolStripMenuItem_Refresh.Click += new System.EventHandler(this.ToolStripMenuItem_RefreshTool_Click);
            // 
            // toolStripMenuItem_Chart
            // 
            this.toolStripMenuItem_Chart.Image = global::Stonks.Properties.Resources.linechart;
            this.toolStripMenuItem_Chart.Name = "toolStripMenuItem_Chart";
            this.toolStripMenuItem_Chart.Size = new System.Drawing.Size(64, 23);
            this.toolStripMenuItem_Chart.Text = "Chart";
            this.toolStripMenuItem_Chart.Click += new System.EventHandler(this.ToolStripMenuItem_Chart_Click);
            // 
            // toolStripComboBox_ChartValues
            // 
            this.toolStripComboBox_ChartValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox_ChartValues.Name = "toolStripComboBox_ChartValues";
            this.toolStripComboBox_ChartValues.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox_ChartValues.SelectedIndexChanged += new System.EventHandler(this.ToolStripComboBox_ChartValues_SelectedIndexChanged);
            // 
            // timer_ClockTitle
            // 
            this.timer_ClockTitle.Enabled = true;
            this.timer_ClockTitle.Interval = 1000;
            this.timer_ClockTitle.Tick += new System.EventHandler(this.Timer_ClockTitle_Tick);
            // 
            // timer_Chart
            // 
            this.timer_Chart.Enabled = true;
            this.timer_Chart.Interval = 60000;
            this.timer_Chart.Tick += new System.EventHandler(this.Timer_Chart_Tick);
            // 
            // chart
            // 
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.LabelStyle.Angle = -45;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.Name = "chartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.DataBindings.Add(new System.Windows.Forms.Binding("Visible", global::Stonks.Properties.Settings.Default, "DisplayCharts", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chart.Dock = System.Windows.Forms.DockStyle.Top;
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(0, 27);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(944, 300);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart1";
            this.chart.Visible = global::Stonks.Properties.Settings.Default.DisplayCharts;
            this.chart.MouseLeave += new System.EventHandler(this.Chart_MouseLeave);
            this.chart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Chart_MouseMove);
            // 
            // toolStripStatusLabel_AvgPercentChange
            // 
            this.toolStripStatusLabel_AvgPercentChange.AutoSize = false;
            this.toolStripStatusLabel_AvgPercentChange.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel_AvgPercentChange.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.toolStripStatusLabel_AvgPercentChange.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel_AvgPercentChange.Name = "toolStripStatusLabel_AvgPercentChange";
            this.toolStripStatusLabel_AvgPercentChange.Size = new System.Drawing.Size(185, 24);
            this.toolStripStatusLabel_AvgPercentChange.Text = "Avg %Change: 0.00%";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(944, 675);
            this.Controls.Add(this.dataGridView_StockList);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.statusStrip_1);
            this.Controls.Add(this.menuStrip_1);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip_1;
            this.MinimumSize = new System.Drawing.Size(900, 256);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stonks";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.statusStrip_1.ResumeLayout(false);
            this.statusStrip_1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_StockList)).EndInit();
            this.menuStrip_1.ResumeLayout(false);
            this.menuStrip_1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private StatusStrip statusStrip_1;
        private ToolStripStatusLabel toolStripStatusLabel_Value;
        private ToolStripStatusLabel toolStripStatusLabel_Gain;
        private ToolStripStatusLabel toolStripStatusLabel_Paid;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private DataGridView dataGridView_StockList;
        private MenuStrip menuStrip_1;
        private ToolStripMenuItem toolStripMenuItem_Refresh;
        private ToolStripMenuItem toolStripMenuItem_Chart;
        private Timer timer_ClockTitle;
        private ToolStripMenuItem toolStripMenuItem_File;
        private ToolStripMenuItem toolStripMenuItem_Exit;
        private ToolStripMenuItem toolStripMenuItem_LogViewer;
        private ToolStripMenuItem toolStripMenuItem_About;
        private ToolStripSeparator toolStripSeparator_1;
        private ToolStripMenuItem toolStripMenuItem_Settings;
        private Timer timer_Chart;
        private ToolStripComboBox toolStripComboBox_ChartValues;
        private DataGridViewCheckBoxColumn colChart;
        private DataGridViewTextBoxColumn colSymbol;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colCurrent;
        private DataGridViewTextBoxColumn colPercent;
        private DataGridViewTextBoxColumn colShares;
        private DataGridViewTextBoxColumn colValue;
        private DataGridViewTextBoxColumn colPaid;
        private DataGridViewTextBoxColumn colGain;
        private DataGridViewTextBoxColumn colGainPercent;
        private DataGridViewTextBoxColumn col52High;
        private DataGridViewTextBoxColumn col52Low;
        private DataGridViewTextBoxColumn colNotes;
        private ToolStripStatusLabel toolStripStatusLabel_AvgPercentChange;
    }
}
