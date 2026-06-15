using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;
using System.Xml.Linq;

namespace RdpAutoClickNew.Services
{
    internal class RdpInteractionService
    {
        private static AutomationElement window;

        /*
         * Launches the incoming rdp path and attempts to wait for 
         * security popup by using the known title
         * 
         * Returns bool whether success or not
         * 
         */
        public static bool GetRdpWindow(string rdpPath)
        {
            LaunchRdp(rdpPath);

            window = WaitForWindow( 15000);

            return window != null;
        }

        // Win32: launch process
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr ShellExecute(
            IntPtr hwnd,
            string lpOperation,
            string lpFile,
            string lpParameters,
            string lpDirectory,
            int nShowCmd);

        static void LaunchRdp(string path)
        {
            ShellExecute(IntPtr.Zero, "open", "mstsc.exe", "\"" + path + "\"", null, 1);
        }

        /*
         * Searches AutomationElement root element's children for a element with matching title
         * 
         * e.g. a window named @title
         */

        static AutomationElement WaitForWindow( int timeout)
        {
            int elapsed = 0;

            while (elapsed < timeout)
            {
                AutomationElementCollection windows =
                    AutomationElement.RootElement.FindAll(
                        TreeScope.Children,
                        Condition.TrueCondition);

                foreach (AutomationElement win in windows)
                {
                    try
                    {
                        var formTypeClassName = "#32770";
                        if (win.Current.ClassName != formTypeClassName)
                            continue;

                        string processName =
                            Process.GetProcessById(win.Current.ProcessId).ProcessName;

                        if (!processName.Equals("mstsc",
                            StringComparison.OrdinalIgnoreCase))
                            continue;

                        Thread.Sleep(50);
                        return win;
                    }
                    catch
                    {
                        // Window may have closed while inspecting it
                    }
                }

                Thread.Sleep(50);
                elapsed += 50;
            }

            return null;
        }
        /*
         * Searches an established window for all checkboxes and does either 
         * 
         * reportOrGet = 0 : output as a user message all the names and ids
         * reportOrGet = 1 : return a list of the ids found programmatically
         * 
         * 
         */
        public static List<string> ProcessAllAvailableCheckboxes( int reportOrGet = 0)
        {
            if (reportOrGet > 1 || reportOrGet < 0)
                Message(LanguageService.T("InvalidProcessType") + ": '" + reportOrGet.ToString() + "'");
            
            var checkboxes = window.FindAll(
                                    TreeScope.Descendants,
                                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.CheckBox)
                                );

            var report = ($"Found {checkboxes.Count} checkboxes:\n");
            List<string> all = new List<string>();
            int cnt = 1;
            foreach (AutomationElement cb in checkboxes)
            {
                all.Add(cb.Current.AutomationId);

                report += Environment.NewLine + Environment.NewLine + cnt.ToString() + Environment.NewLine + ($"Name: {cb.Current.Name}") +
                Environment.NewLine + ($"AutomationId: {cb.Current.AutomationId}") +
                Environment.NewLine + ("-------------------------");
                cnt++;
            }

            const uint MB_ICONINFORMATION = 0x00000040;
            if (reportOrGet == 0)
                Message(report, MB_ICONINFORMATION);
            else
                return all;

            return null;

        }
        const uint MB_ICONERROR = 0x00000010;

        // wrapper
        public static void Message(string msg, uint type = MB_ICONERROR)
        {
            Program.Message(msg, type);
        }
       
        /*
         * Uses the incoming search conditions to find an automation element 
         * expected to be a checkbox to toggle ON
         * 
         * 
         */
        public  static bool ToggleCheckbox( PropertyCondition searchCondition)
        {
            var searchValue = searchCondition.Value;
            try
            {
                AutomationElement el = window.FindFirst(
                    TreeScope.Descendants,
                    searchCondition);


                if (el == null)
                {
                    Message(LanguageService.T("CheckboxNotFound") + ": '" + searchValue + "'");
                    return false;
                }

                if (el.Current.ControlType != ControlType.CheckBox)
                {
                    Message(LanguageService.T("NotCheckbox") + ": '" + searchValue + "'");
                    return false;
                }

                try
                {
                    object pattern = el.GetCurrentPattern(TogglePattern.Pattern);
                    ((TogglePattern)pattern).Toggle();
                    return true;
                }
                catch
                {
                    Message(LanguageService.T("CheckboxNotInteractable") + ": '" + searchValue + "'");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Program.Message("Error toggling ID/Name'" + searchValue + "': " + ex.Message);
                return false;
            }
        }
        public static bool ToggleCheckboxById(string id)
        {
            var searchCond = new PropertyCondition(AutomationElement.AutomationIdProperty, id);
            return ToggleCheckbox(searchCond);


        }
        public static bool ToggleCheckboxByName(string name)
        {
            var searchCond = new PropertyCondition(AutomationElement.NameProperty, name);
            return ToggleCheckbox(searchCond);

        }
        public static bool ClickButtonById( string buttonId)
        {
            try
            {
                AutomationElement el = window.FindFirst(
                    TreeScope.Descendants,
                    new PropertyCondition(AutomationElement.AutomationIdProperty, buttonId));

                if (el == null)
                {
                    Message(LanguageService.T("ButtonNotFound") + ": id='" + buttonId + "'");
                    return false;
                }

                try
                {
                    object pattern = el.GetCurrentPattern(InvokePattern.Pattern);
                    ((InvokePattern)pattern).Invoke();
                    
                    return true;
                }
                catch
                {
                    Message(LanguageService.T("ButtonNotClickable") + ": id='" + buttonId + "'");
                    return false;
                }
            }
            catch (Exception ex)
            {

                Message("Error clicking '" + ": id='" + buttonId + ex.Message);
                return false;
            }
        }

    }
}
