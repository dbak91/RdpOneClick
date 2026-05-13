using System;
using System.Windows.Forms;

namespace RdpShortcutCreator
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new ShortcutCreatorForm());
        }
    }
}