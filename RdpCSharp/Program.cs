using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

class RdpAutoClick
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern void SetCursorPos(int X, int Y);

    [DllImport("user32.dll")]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInfo);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr FindWindow(string? lpClassName, string lpWindowName);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    private const uint MOUSEEVENTF_LEFTDOWN = 0x2;
    private const uint MOUSEEVENTF_LEFTUP = 0x4;
    private const uint MB_OK = 0;
    private const uint MB_ICONERROR = 0x10;

    static bool WaitForWindow(string title, int timeoutMs)
    {
        int elapsed = 0;
        int interval = 100;

        while (elapsed < timeoutMs)
        {
            IntPtr hWnd = FindWindow(null, title);
            if (hWnd != IntPtr.Zero)
                return true;

            Thread.Sleep(interval);
            elapsed += interval;
        }
        return false;
    }

    static void ClickAt(int x, int y)
    {
        SetCursorPos(x, y);
        Thread.Sleep(50);
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
    }

    static void ShowUsage()
    {
        MessageBox(IntPtr.Zero,
            "Usage:" + Environment.NewLine +
            "Click mode:" + Environment.NewLine +
            "  RdpAutoClick.exe click <rdpX> <rdpY> <connectX> <connectY> [preX preY]..." + Environment.NewLine +
            "Path mode:" + Environment.NewLine +
            "  RdpAutoClick.exe <rdpPath> <connectX> <connectY> [preX preY]...",
            "RDP AutoClick",
            MB_OK | MB_ICONERROR);
    }

    [STAThread]
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            ShowUsage();
            return;
        }

        int connectX = 0;
        int connectY = 0;
        int startIndex = 0;

        // ===============================
        // CLICK MODE
        // ===============================
        if (args[0].ToLower() == "click")
        {
            if (args.Length < 5)
            {
                ShowUsage();
                return;
            }

            if (!int.TryParse(args[1], out int rdpX) || !int.TryParse(args[2], out int rdpY))
            {
                MessageBox(IntPtr.Zero, "Invalid RDP click coordinates.", "RDP AutoClick", MB_OK | MB_ICONERROR);
                return;
            }

            if (!int.TryParse(args[3], out connectX) || !int.TryParse(args[4], out connectY))
            {
                MessageBox(IntPtr.Zero, "Invalid connect coordinates.", "RDP AutoClick", MB_OK | MB_ICONERROR);
                return;
            }

            // Double-click the .rdp file
            ClickAt(rdpX, rdpY);
            Thread.Sleep(100);
            ClickAt(rdpX, rdpY);

            if (!WaitForWindow("Remote Desktop Connection security warning", 5000))
            {
                MessageBox(IntPtr.Zero, "RDP window did not appear.", "RDP AutoClick", MB_OK | MB_ICONERROR);
                return;
            }

            startIndex = 5;
        }
        else
        {
            // ===============================
            // PATH MODE
            // ===============================
            if (args.Length < 3)
            {
                ShowUsage();
                return;
            }

            string rdpPath = args[0];

            if (!int.TryParse(args[1], out connectX) || !int.TryParse(args[2], out connectY))
            {
                MessageBox(IntPtr.Zero, "Invalid connect coordinates.", "RDP AutoClick", MB_OK | MB_ICONERROR);
                return;
            }

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "mstsc.exe",
                    Arguments = "\"" + rdpPath + "\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox(IntPtr.Zero, "Failed to start mstsc: " + ex.Message, "RDP AutoClick", MB_OK | MB_ICONERROR);
                return;
            }

            if (!WaitForWindow("Remote Desktop Connection security warning", 5000))
            {
                MessageBox(IntPtr.Zero, "RDP window did not appear.", "RDP AutoClick", MB_OK | MB_ICONERROR);
                return;
            }

            startIndex = 3;
        }

        // ===============================
        // OPTIONAL PRE-CLICKS
        // ===============================
        int i = startIndex;
        while (i + 1 < args.Length)
        {
            if (!int.TryParse(args[i], out int preX) || !int.TryParse(args[i + 1], out int preY))
            {
                MessageBox(IntPtr.Zero, "Invalid pre-click coordinates at position " + i, "RDP AutoClick", MB_OK | MB_ICONERROR);
                return;
            }

            ClickAt(preX, preY);
            Thread.Sleep(100);

            i += 2;
        }

        // ===============================
        // FINAL CONNECT CLICK
        // ===============================
        ClickAt(connectX, connectY);
    }
}
