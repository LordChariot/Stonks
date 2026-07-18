namespace Stonks
{
    partial class LogViewerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogViewerForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_OpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Wrap = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBox_LogType = new System.Windows.Forms.ToolStripComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_File,
            this.toolStripMenuItem_Refresh,
            this.toolStripMenuItem_Wrap,
            this.comboBox_LogType});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem_File
            // 
            this.toolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_OpenFolder});
            this.toolStripMenuItem_File.Name = "toolStripMenuItem_File";
            this.toolStripMenuItem_File.Size = new System.Drawing.Size(37, 23);
            this.toolStripMenuItem_File.Text = "&File";
            // 
            // toolStripMenuItem_OpenFolder
            // 
            this.toolStripMenuItem_OpenFolder.Image = global::Stonks.Properties.Resources.OpenFolder;
            this.toolStripMenuItem_OpenFolder.Name = "toolStripMenuItem_OpenFolder";
            this.toolStripMenuItem_OpenFolder.Size = new System.Drawing.Size(218, 22);
            this.toolStripMenuItem_OpenFolder.Text = "&Open Folder in File Explorer";
            this.toolStripMenuItem_OpenFolder.Click += new System.EventHandler(this.ToolStripMenuItem_OpenFolder_Click);
            // 
            // toolStripMenuItem_Refresh
            // 
            this.toolStripMenuItem_Refresh.Image = global::Stonks.Properties.Resources.RefreshScript;
            this.toolStripMenuItem_Refresh.Name = "toolStripMenuItem_Refresh";
            this.toolStripMenuItem_Refresh.Size = new System.Drawing.Size(74, 23);
            this.toolStripMenuItem_Refresh.Text = "&Refresh";
            this.toolStripMenuItem_Refresh.Click += new System.EventHandler(this.ToolStripMenuItem_Refresh_Click);
            // 
            // toolStripMenuItem_Wrap
            // 
            this.toolStripMenuItem_Wrap.Image = global::Stonks.Properties.Resources.WrapPanel;
            this.toolStripMenuItem_Wrap.Name = "toolStripMenuItem_Wrap";
            this.toolStripMenuItem_Wrap.Size = new System.Drawing.Size(63, 23);
            this.toolStripMenuItem_Wrap.Text = "&Wrap";
            this.toolStripMenuItem_Wrap.Click += new System.EventHandler(this.ToolStripMenuItem_Wrap_Click);
            // 
            // comboBox_LogType
            // 
            this.comboBox_LogType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_LogType.Name = "comboBox_LogType";
            this.comboBox_LogType.Size = new System.Drawing.Size(121, 23);
            this.comboBox_LogType.SelectedIndexChanged += new System.EventHandler(this.ComboBox_LogType_SelectedIndexChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.DetectUrls = false;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 27);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(784, 534);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.WordWrap = false;
            // 
            // LogViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "LogViewerForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Log Viewer";
            this.Load += new System.EventHandler(this.LogViewerForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Refresh;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Wrap;
        private System.Windows.Forms.ToolStripComboBox comboBox_LogType;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_File;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_OpenFolder;
    }
}