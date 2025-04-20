using System;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;

namespace BonfieFood
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (new Login().AutomaticLogin())
            {                
                string savedLanguage = Properties.Settings.Default.Language;
                Thread.CurrentThread.CurrentCulture = new CultureInfo(savedLanguage);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(savedLanguage);

                Application.Run(new Home());
            }
            else
            {
                string defaultLanguage = Properties.Settings.Default.Language = "en";
                Thread.CurrentThread.CurrentCulture = new CultureInfo(defaultLanguage);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(defaultLanguage);
                Application.Run(new Login());
            }
        }
    }
}