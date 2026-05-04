using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;
using System.Runtime.InteropServices;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Message("Missing RDP path");
            return;
        }

        LaunchRdp(args[0]);

        var window = WaitForWindow("Remote Desktop Connection security warning", 15000);

        if (window == null)
        {
            Message("RDP window not found");
            return;
        }

        window.SetFocus();
        Thread.Sleep(300);

        for (int i = 1; i < args.Length; i++)
        {
            ToggleCheckboxByName(window, args[i]);
        }

        if (!ClickByName(window, "Connect"))
        {
            Message("Failed to click Connect button");
        }
    }

    static void LaunchRdp(string path)
    {
        Process.Start("mstsc.exe", "\"" + path + "\"");
    }

    static AutomationElement WaitForWindow(string title, int timeout)
    {
        int t = 0;

        while (t < timeout)
        {
            var root = AutomationElement.RootElement;

            var win = root.FindFirst(
                TreeScope.Children,
                new PropertyCondition(AutomationElement.NameProperty, title));

            if (win != null)
                return win;

            Thread.Sleep(200);
            t += 200;
        }

        return null;
    }

    static bool ToggleCheckboxByName(AutomationElement parent, string name)
    {
        try
        {
            var el = parent.FindFirst(
                TreeScope.Descendants,
                new PropertyCondition(AutomationElement.NameProperty, name));

            if (el == null)
            {
                Message("Checkbox not found: '" + name + "'");
                return false;
            }

            if (el.Current.ControlType != ControlType.CheckBox)
            {
                Message("Not a checkbox: '" + name + "'");
                return false;
            }

            object patternObj;

            try
            {
                patternObj = el.GetCurrentPattern(TogglePattern.Pattern);
                ((TogglePattern)patternObj).Toggle();
                return true;
            }
            catch
            {
                Message("Checkbox not interactable: '" + name + "'");
                return false;
            }
        }
        catch (Exception ex)
        {
            Message("Error toggling '" + name + "': " + ex.Message);
            return false;
        }
    }

    static bool ClickByName(AutomationElement parent, string name)
    {
        try
        {
            var el = parent.FindFirst(
                TreeScope.Descendants,
                new PropertyCondition(AutomationElement.NameProperty, name));

            if (el == null)
            {
                Message("Button not found: '" + name + "'");
                return false;
            }

            try
            {
                object patternObj = el.GetCurrentPattern(InvokePattern.Pattern);
                ((InvokePattern)patternObj).Invoke();
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

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int MessageBoxW(IntPtr hWnd, string text, string caption, uint type);

    private const uint MB_OK = 0;
    private const uint MB_ICONERROR = 0x10;

    public static void Message(string msg)
    {
        MessageBoxW(IntPtr.Zero, msg, "RDP AutoClick", MB_OK | MB_ICONERROR);
    }
}