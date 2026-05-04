using System;
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

        AutomationElement window = WaitForWindow("Remote Desktop Connection security warning", 15000);

        if (window == null)
        {
            Message("RDP window not found");
            return;
        }

        //window.SetFocus();
        //Thread.Sleep(300);

        for (int i = 1; i < args.Length; i++)
        {
            ToggleCheckboxByName(window, args[i]);
        }

        if (!ClickByName(window, "Connect"))
        {
            Message("Failed to click Connect button");
        }
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

    static bool ToggleCheckboxByName(AutomationElement parent, string name)
    {
        try
        {
            AutomationElement el = parent.FindFirst(
                TreeScope.Descendants,
                new PropertyCondition(AutomationElement.NameProperty, name));

            if (el == null)
            {
                Message("Checkbox not found: '" + name + "'");
                return false;
            }

            if (el.Current.ControlType != ControlType.CheckBox)
            {
                Message("Not a checkbox: '" + name + "'");//
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

    private const uint MB_OK = 0;
    private const uint MB_ICONERROR = 0x10;

    static void Message(string msg)
    {
        MessageBoxW(IntPtr.Zero, msg, "RDP AutoClick", MB_OK | MB_ICONERROR);
    }
}