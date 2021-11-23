using System;
using System.Windows.Forms;

namespace WinUpdateTool
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main(Math.Pow(2, 0), args));
        }
    }
}
