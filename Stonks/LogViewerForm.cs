using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Stonks
{
    public partial class LogViewerForm : Form
    {
        private string _logFile = Logger.LogFile;

        public LogViewerForm()
        {
            InitializeComponent();
        }

        private void LogViewerForm_Load(object sender, EventArgs e)
        {
            foreach (var logType in Enum.GetValues(typeof(LogTypes)))
            {
                comboBox_LogType.Items.Add(logType.ToString());
            }
            comboBox_LogType.SelectedIndex = Properties.Settings.Default.LogType;
            ViewLog();
        }

        private void ComboBox_LogType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.LogType = comboBox_LogType.SelectedIndex;
            ViewLog();
        }
        private void ToolStripMenuItem_Refresh_Click(object sender, EventArgs e)
        {
            ViewLog();
        }
        private void ToolStripMenuItem_Wrap_Click(object sender, EventArgs e)
        {
            richTextBox1.WordWrap = !richTextBox1.WordWrap;
            toolStripMenuItem_Wrap.Text = richTextBox1.WordWrap ? "Unwrap" : "Wrap";
        }

        private void ViewLog()
        {
            switch (comboBox_LogType.SelectedIndex)
            {
                case 0:
                    _logFile = Logger.LogFile;
                    break;
                case 1:
                    _logFile = Logger.ChartDataFile;
                    break;
                default:
                    _logFile = Logger.LogFile;
                    break;
            }

            this.Text = $"Log Viewer - {_logFile}";
            if (!File.Exists(_logFile))
            {
                richTextBox1.Text = $"Log file not found: {_logFile}";
                return;
            }
            richTextBox1.Clear();
            richTextBox1.AppendText(File.ReadAllText(_logFile));

            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.SelectionLength = 0;
            richTextBox1.ScrollToCaret();
        }

        private void ToolStripMenuItem_OpenFolder_Click(object sender, EventArgs e)
        {
            var _folder = Path.GetDirectoryName(_logFile);
            if (Directory.Exists(_folder))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = _folder + Path.DirectorySeparatorChar,
                    FileName = "explorer.exe",
                    UseShellExecute = true,
                    Verb = "open"
                };
                Process.Start(startInfo);
            }
        }
    }
}
