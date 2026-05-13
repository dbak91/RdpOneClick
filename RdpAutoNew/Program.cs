using RdpAutoClickNew.Services;
using System;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    /*
     * Entry point. 
     * 
     * Orchestrates logic from arguments and uses RdpInteractionService for all
     * handling of the rdp window
     * 
     * - establish window
     * - if arg = -showIds, process all available checkboxes
     * - if arg = -usage, show prameter usage
     * - if arg = -all, get all ids and toggle checkboxes
     * - else arg = names or ids, if int toggle by id else toggle by name
     * - press connect button
     * 
     */
    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Message("No Parameters. Missing RDP path'"+Environment.NewLine +"'-usage' To see parameter usage");
            return;
        }

        var establishedWindow = RdpInteractionService.GetRdpWindow(args[0]);

        if (!establishedWindow)
        {
            Message("RDP window not found");
            return;
        }

        // Show available checkboxes
        if (args.Length > 1 && args[1] == "-showIds")
        {
            RdpInteractionService.ProcessAllAvailableCheckboxes( 0);
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
            var allIds = RdpInteractionService.ProcessAllAvailableCheckboxes( 1);
            
            foreach (var id in allIds)
            {

                RdpInteractionService.ToggleCheckboxById(id);
            }
        }// Toggle bya args
        else
            for (int i = 1; i < args.Length; i++)
            {

                if (int.TryParse(args[i], out var tmp))
                {
                    RdpInteractionService.ToggleCheckboxById( args[i]);
                }
                else
                    RdpInteractionService.ToggleCheckboxByName(args[i]);

               
            }
        Thread.Sleep(50);
        if (!RdpInteractionService.ClickByName( "Connect"))
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
    // Win32: message box
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int MessageBoxW(IntPtr hWnd, string text, string caption, uint type);

    const uint MB_OK = 0x00000000;
    const uint MB_ICONERROR = 0x00000010;
    const uint MB_ICONWARNING = 0x00000030;
    const uint MB_ICONINFORMATION = 0x00000040;
    public static void Message(string msg, uint type = MB_ICONERROR)
    {
        MessageBoxW(IntPtr.Zero, msg, "RDP AutoClick", MB_OK | type);
    }
}