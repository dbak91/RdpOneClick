using RdpAutoClickNew.Services;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Linq;

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
            Message(LanguageService.T("NoParameters"));
            return;
        }
        if (args.Length >= 1 && args[0] == "-usage")
        {
            ShowUsage();
            return;
        }

        var establishedWindow = RdpInteractionService.GetRdpWindow(args[0]);

        if (!establishedWindow)
        {
            Message(LanguageService.T("WindowNotFound"));
            return;
        }

        // Show available checkboxes
        if (args.Length > 1 && args[1] == "-showIds")
        {
            RdpInteractionService.ProcessAllAvailableCheckboxes( 0);
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
        if (!RdpInteractionService.ClickButtonById( "1"))
        {
            Message(LanguageService.T("ConnectButtonFailed"));
        }
    }

    private static void ShowUsage()
    {
        Message(LanguageService.T("Usage") , MB_ICONINFORMATION);
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