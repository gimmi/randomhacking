﻿using System;
using System.Windows.Forms;

namespace SpikeCefSharp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Backend backend = new Backend();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(backend));
        }
    }
}
