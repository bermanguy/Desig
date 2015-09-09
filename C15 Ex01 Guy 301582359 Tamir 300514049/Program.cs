using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace C15_Ex01_Guy_301582359_Tamir_300514049
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Facebook_Form());


        }
    }
}
