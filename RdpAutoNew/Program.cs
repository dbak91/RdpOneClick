using System;
using System.Threading;
using System.Windows.Automation;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Message("No Parameters. Missing RDP path'"+Environment.NewLine +"'-usage' To see parameter usage");
            return;
        }

        LaunchRdp(args[0]);

        AutomationElement window = WaitForWindow("Remote Desktop Connection security warning", 15000);

        if (window == null)
        {
            Message("RDP window not found");
            return;
        }

        // Show available checkboxes
        if (args.Length > 1 && args[1] == "-showIds")
        {
            ProcessAllAvailableCheckboxes(window, 0);
            return;
        }
        if (args.Length > 1 && args[1] == "-usage")
        {
            ShowUsage();
            return;
        }
        // Get all checkbox Ids and toggle
        if (args.Length > 1 && args[1] == "-all")
        {
            var allIds = ProcessAllAvailableCheckboxes(window, 1);
            foreach (var id in allIds)
            {
                ToggleCheckboxById(window,id);
            }
        }// Toggle bya args
        else
            for (int i = 1; i < args.Length; i++)
            {
                if (int.TryParse(args[i], out var tmp))
                {
                    ToggleCheckboxById(window, args[i]);
                }
                else
                    ToggleCheckboxByName(window, args[i]);
            }

        if (!ClickByName(window, "Connect"))
        {
            Message("Failed to click Connect button");
        }
    }

    private static void ShowUsage()
    {
        var usg = "Usage" + Environment.NewLine +
                  "-----" +
                  Environment.NewLine + Environment.NewLine +
                  "'RdpAutoClick.exe <RdpPath> <Optional Checkbox Names or AutomationIds>'" +
                  Environment.NewLine + "   E.g 'RdpAutoClick.exe Clipboard Drives  16553'" +
                  Environment.NewLine + Environment.NewLine +
                  "'RdpAutoClick.exe -usage'" +
                  Environment.NewLine + "   This will show this message and explain exe usage" +
                  Environment.NewLine + Environment.NewLine +
                  "'RdpAutoClick.exe -all'" +
                  Environment.NewLine + "   This will select all checkboxes before connecting" +

                  Environment.NewLine + Environment.NewLine +
                  "'RdpAutoClick.exe <RdpPath> -showIds'" +
                  Environment.NewLine + "   This will report back the names and ids of all available Checkboxes'";

                 

        Message(usg, MB_ICONINFORMATION);
    }

    // Process all checkboxes, either returns is or prints names. 
    private static List<string> ProcessAllAvailableCheckboxes(AutomationElement window,int reportOrGet=0)
    {
        if(reportOrGet>1 || reportOrGet<0)
            Message("Error, program has passed an invalid type to ProcessAllAvailableCheckboxes ("+reportOrGet.ToString()+"), when only 0 or 1 accepted");

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
            Environment.NewLine + ($"AutomationId: {cb.Current.AutomationId}")+
            Environment.NewLine + ("-------------------------");
            cnt++;
        }
        if (reportOrGet == 0)
            Message(report,MB_ICONINFORMATION);
        else
            return all;

        return null;

    }

    // Replace Process.Start with ShellExecute
    static void LaunchRdp(string path)
    {
        ShellExecute(IntPtr.Zero, "open", "mstsc.exe", "\"" + path + "\"", null, 1);
    }

    static AutomationElement WaitForWindow(string title, int timeout)
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
    static bool ToggleCheckbox(AutomationElement parent, PropertyCondition searchCondition)
    {
        var searchValue = searchCondition.Value;
        try
        {
            AutomationElement el = parent.FindFirst(
                TreeScope.Descendants,
                searchCondition);

            
            if (el == null)
            {
                Message("Checkbox not found: ID/Name:'"+searchValue+"'");
                return false;
            }

            if (el.Current.ControlType != ControlType.CheckBox)
            {
                Message("Not a checkbox: ID/Name:'" + searchValue + "'");//
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
                Message("Checkbox not interactable: ID/Name:'" + searchValue + "'");
                return false;
            }
        }
        catch (Exception ex)
        {
            Message("Error toggling ID/Name'" + searchValue + "': " + ex.Message);
            return false;
        }
    }
    static bool ToggleCheckboxById(AutomationElement parent, string id)
    {
        var searchCond = new PropertyCondition(AutomationElement.AutomationIdProperty, id);
        return ToggleCheckbox(parent, searchCond);


    }
    static bool ToggleCheckboxByName(AutomationElement parent, string name)
    {
        var searchCond = new PropertyCondition(AutomationElement.NameProperty, name);
        return ToggleCheckbox(parent, searchCond);

    }

    static bool ClickByName(AutomationElement parent, string name)
    {
        try
        {
            AutomationElement el = parent.FindFirst(
                TreeScope.Descendants,
                new PropertyCondition(AutomationElement.NameProperty, name));

            if (el == null)
            {
                Message("Button not found: '" + name + "'");
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
                Message("Button not clickable: '" + name + "'");
                return false;
            }
        }
        catch (Exception ex)
        {
            Message("Error clicking '" + name + "': " + ex.Message);
            return false;
        }
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

    // Win32: message box
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int MessageBoxW(IntPtr hWnd, string text, string caption, uint type);

    const uint MB_OK = 0x00000000;
    const uint MB_ICONERROR = 0x00000010;
    const uint MB_ICONWARNING = 0x00000030;
    const uint MB_ICONINFORMATION = 0x00000040;

    static void Message(string msg, uint type = MB_ICONERROR)
    {
        MessageBoxW(IntPtr.Zero, msg, "RDP AutoClick", MB_OK | type);
    }
}