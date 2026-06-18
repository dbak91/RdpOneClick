using RdpAutoClickNew.Services;
using RdpShortcutCreator;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Linq;
using System.Windows.Forms;
class Program
{
    /*
     * Entry point. 
     * 
     * Orchestrates logic from arguments and uses RdpInteractionService for all
     * handling of the rdp window
     * 
     * - Validate min parameters
     * - if arg = -usage, show parameter usage and exit
     * - establish rdp window
     * - if arg = -showIds, process all available checkboxes and report ids to user, then exit
     * - if arg = -all, get all ids and toggle checkboxes
     * - else arg = names or ids, if int toggle by id else toggle by name
     * - press connect button
     * 
     */

    [STAThread]
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Application.EnableVisualStyles();
            Application.Run(new ShortcutCreatorForm());
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
            NativeMethods.Message(LanguageService.T("WindowNotFound"));
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
        }// Toggle by args (ID if int)
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

        var connectButtonId = "1";
        if (!RdpInteractionService.ClickButtonById( connectButtonId))
        {
            NativeMethods.Message(LanguageService.T("ConnectButtonFailed"));
        }
    }

    private static void ShowUsage()
    {
        const uint MB_ICONINFORMATION = 0x00000040;
        
        NativeMethods.Message(LanguageService.T("Usage") , MB_ICONINFORMATION);
    }
    
}