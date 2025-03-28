using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                Application.Run(new Home());
                //Application.Run(new Nutrition());
            }
            else
            {
                Application.Run(new Login());
                //Application.Run(new Home());
                //Application.Run(new Profile());
                //Application.Run(new panelTheCPFC());
            }
        }
    }
}