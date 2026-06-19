using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stonks
{
    class TrayApplicationContext : ApplicationContext
    {
        public static TrayApplicationContext Instance { get; private set; }
        public static List<StockViewModel> Stocks { get; set; } = new List<StockViewModel>();
        private AboutForm _aboutForm;
        private LogViewerForm _logViewerForm;
        private MainForm _mainForm;
        private SettingsForm _settingsForm;
        private NotifyIcon _notifyIcon;
        private PollingEngine _pollingEngine;

        public TrayApplicationContext()
        {
            Instance = this;
            _aboutForm = new AboutForm();
            _logViewerForm = new LogViewerForm();
            _mainForm = new MainForm();
            _settingsForm = new SettingsForm();
            _notifyIcon = new NotifyIcon();

            try
            {
                var icon = Properties.Resources.icon_linechart ?? SystemIcons.Application;
                _notifyIcon.Icon = icon;
            }
            catch
            {
                _notifyIcon.Icon = SystemIcons.Application;
            }
            _notifyIcon.Visible = true;
            _notifyIcon.Text = "Stonks";

            var contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add(new ToolStripMenuItem("Open", Properties.Resources.Stonks, (s, e) => ShowMainWindow(), "Open"));
            contextMenuStrip.Items.Add(new ToolStripMenuItem("Settings", Properties.Resources.Settings, (s, e) => ShowSettings(), "Settings"));
            contextMenuStrip.Items.Add(new ToolStripMenuItem("Log Viewer", Properties.Resources.Log, (s, e) => ShowLogViewer(), "Log Viewer"));
            contextMenuStrip.Items.Add(new ToolStripMenuItem("Refresh Now", Properties.Resources.Refresh, async (s, e) => await RefreshNow(), "Refresh Now"));
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            contextMenuStrip.Items.Add(new ToolStripMenuItem("About", Properties.Resources.AboutBox, (s, e) => ShowAbout(), "About"));
            contextMenuStrip.Items.Add("Exit", Properties.Resources.Exit, (s, e) => Exit());
            contextMenuStrip.Items[0].Font = new Font(contextMenuStrip.Font, contextMenuStrip.Font.Style | FontStyle.Bold);
            _notifyIcon.ContextMenuStrip = contextMenuStrip;
            _notifyIcon.DoubleClick += (s, e) => ShowMainWindow();
            _notifyIcon.MouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    // toggle main window
                    if (_mainForm.Visible)
                        _mainForm.Hide();
                    else
                        ShowMainWindow();
                }
            };

            _pollingEngine = new PollingEngine();
            _pollingEngine.OnUpdate += Engine_OnUpdate;
            _pollingEngine.OnNotification += Engine_OnNotification;
            _pollingEngine.Start();
        }


        private void Engine_OnNotification(object sender, string e)
        {
            try
            {
                _notifyIcon.ShowBalloonTip(5000, "Stonks", e, ToolTipIcon.Info);
                Logger.LogInfo("Notification: " + e);
            }
            catch { }
        }

        private void Engine_OnUpdate(object sender, PollingEngine.UpdateEventArgs e)
        {
            _mainForm.InvokeIfRequired(() => _mainForm.UpdateStockList(e.Stocks));
            Stocks = e.Stocks;
        }

        public void Exit()
        {
            _pollingEngine?.Stop();
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
            Logger.LogInfo($"Performed: Exit()");
            Instance = null;
            Application.Exit();
        }

        public async Task RefreshNow()
        {
            await _pollingEngine.RefreshNowAsync();
            Logger.LogInfo($"Performed: RefreshNow()");
        }
        private void ShowAbout()
        {
            if (_aboutForm.IsDisposed) _aboutForm = new AboutForm();
            _aboutForm.ShowDialog();
            Logger.LogInfo($"Performed: ShowAbout()");
        }

        private void ShowLogViewer()
        {
            if (_logViewerForm.IsDisposed) _logViewerForm = new LogViewerForm();
            _logViewerForm.Show();
            _logViewerForm.BringToFront();
            try { _logViewerForm.Activate(); _logViewerForm.TopMost = true; _logViewerForm.TopMost = false; } catch { Logger.LogError("Failed to activate log viewer form."); }
            Logger.LogInfo($"Performed: ShowLogViewer()");
        }

        private void ShowMainWindow()
        {
            if (_mainForm.IsDisposed) _mainForm = new MainForm();
            _mainForm.Show();
            _mainForm.BringToFront();
            try { _mainForm.Activate(); _mainForm.TopMost = true; _mainForm.TopMost = false; } catch { }
            Logger.LogInfo($"Performed: ShowMainWindow()");
        }

        private void ShowSettings()
        {
            if (_settingsForm.IsDisposed) _settingsForm = new SettingsForm();
            _settingsForm.ShowDialog();
            Logger.LogInfo($"Performed: ShowSettings()");
        }

    }

    static class ControlExtensions
    {
        public static void InvokeIfRequired(this Control control, Action action)
        {
            if (control == null || control.IsDisposed) return;
            if (control.InvokeRequired)
                control.Invoke(action);
            else
                action();
        }


    }
}
