using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;

namespace RdpShortcutCreator
{
    internal static class RdpAutomation
    {
        public static void GetRdpCheckboxData(string rdpPath, out List<string> names, out List<string> ids)
        {
            names = new List<string>();
            ids = new List<string>();

            LaunchRdp(rdpPath);
            AutomationElement window = WaitForWindow("Remote Desktop Connection security warning", 15000);

            if (window == null)
            {
                Message("RDP window not found");
                return;
            }

            var checkboxes = window.FindAll(
                TreeScope.Descendants,
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.CheckBox));

            foreach (AutomationElement cb in checkboxes)
            {
                names.Add(cb.Current.Name);
                ids.Add(cb.Current.AutomationId);
            }

            try
            {
                var windowPattern = window.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
                windowPattern?.Close();
            }
            catch { }
        }

        private static void LaunchRdp(string path)
        {
            NativeMethods.ShellExecute(IntPtr.Zero, "open", "mstsc.exe", "\"" + path + "\"", null, 1);
        }

        private static AutomationElement WaitForWindow(string title, int timeout)
        {
            int elapsed = 0;

            while (elapsed < timeout)
            {
                AutomationElement root = AutomationElement.RootElement;

                AutomationElement win = root.FindFirst(
                    TreeScope.Children,
                    new PropertyCondition(AutomationElement.NameProperty, title));

                if (win != null)
                {
                    Thread.Sleep(150);
                    return win;
                }

                Thread.Sleep(200);
                elapsed += 200;
            }

            return null;
        }

        private static void Message(string msg)
        {
            NativeMethods.MessageBoxW(IntPtr.Zero, msg, "RDP AutoClick",
                NativeMethods.MB_OK | NativeMethods.MB_ICONERROR);
        }
    }
}