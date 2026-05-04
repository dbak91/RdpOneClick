using System;
using System.Threading;
using System.Windows.Automation;

class Program
{
    static void Main()
    {
        Console.WriteLine("Searching for target window...");

        AutomationElement targetWindow = null;

        // Retry loop (window might not exist immediately)
        for (int i = 0; i < 10; i++)
        {
            targetWindow = FindWindowByTitle("Remote Desktop Connection security warning");

            if (targetWindow != null)
                break;

            Thread.Sleep(1000);
        }

        if (targetWindow == null)
        {
            Console.WriteLine("Target window not found.");
            return;
        }

        Console.WriteLine("Window found:");
        Console.WriteLine($"Name: {targetWindow.Current.Name}");
        Console.WriteLine("----------------------------------");

        // Find all buttons inside this window ONLY
        var buttons = targetWindow.FindAll(
            TreeScope.Descendants,
            new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button)
        );

        Console.WriteLine($"Found {buttons.Count} buttons:\n");

        foreach (AutomationElement button in buttons)
        {
            Console.WriteLine($"Name: {button.Current.Name}");
            Console.WriteLine($"AutomationId: {button.Current.AutomationId}");
            Console.WriteLine("-------------------------");
        }

        var checkboxes = targetWindow.FindAll(
    TreeScope.Descendants,
    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.CheckBox)
);

        Console.WriteLine($"Found {checkboxes.Count} checkboxes:\n");

        foreach (AutomationElement cb in checkboxes)
        {
            Console.WriteLine($"Name: {cb.Current.Name}");
            Console.WriteLine($"AutomationId: {cb.Current.AutomationId}");
            Console.WriteLine("-------------------------");
        }

        Console.WriteLine("Done.");
        Console.ReadKey();
    }

    static AutomationElement FindWindowByTitle(string partialTitle)
    {
        var root = AutomationElement.RootElement;

        var windows = root.FindAll(TreeScope.Children, Condition.TrueCondition);

        foreach (AutomationElement window in windows)
        {
            string name = window.Current.Name;

            if (!string.IsNullOrEmpty(name) &&
                name.IndexOf(partialTitle, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return window;
            }
        }

        return null;
    }
}