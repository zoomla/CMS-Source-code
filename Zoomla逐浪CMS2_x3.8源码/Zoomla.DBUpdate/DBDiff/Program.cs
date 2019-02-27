using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DBDiff.Front;

namespace DBDiff
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BasicForm());// Form1 
        }
    }
}