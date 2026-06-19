using System;
using System.Windows.Forms;

namespace Stonks
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isFirstInstance;
            const string mutexName = "£ordÇhariot_StonksAppMutex_{e2e2e2e2-e2e2-e2e2-e2e2-e2e2e2e2e2e2}";
            using (var mutex = new System.Threading.Mutex(true, mutexName, out isFirstInstance))
            {
                if (!isFirstInstance)
                {
                    MessageBox.Show("Another instance of Stonks is already running.", "Stonks", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Start the tray application context which manages the notify icon and app lifetime
                Application.Run(new TrayApplicationContext());
            }
        }
    }
}
