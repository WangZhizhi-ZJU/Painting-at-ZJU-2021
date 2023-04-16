using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Painting_at_ZJU
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            WelcomeForm welcomeForm = new WelcomeForm();
            welcomeForm.ShowDialog();

            if(welcomeForm.DialogResult == DialogResult.OK)
            {
                Application.Run(new MainForm());
            }
        }
    }
}
