using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

class RdpAutoClick
{
    [DllImport("user32.dll")]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    static extern IntPtr FindWindowEx(IntPtr parent, IntPtr childAfter, string className, string windowText);

    [DllImport("user32.dll")]
    static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll")]
    static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    const int BM_CLICK = 0x00F5;
    const uint MB_OK = 0x00000000;
    const uint MB_ICONERROR = 0x00000010;

    static string WINDOW_TITLE = "Remote Desktop Connection";
    static string CONNECT_BUTTON = "Connect";

    static void Error(string msg)
    {
        MessageBox(IntPtr.Zero, msg, "RDP AutoClick", MB_OK | MB_ICONERROR);
    }

    static void Info(string msg)
    {
        MessageBox(IntPtr.Zero, msg, "RDP AutoClick", MB_OK);
    }

    static void LaunchRdp(string path)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "mstsc.exe",
                Arguments = "\"" + path + "\"",
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Error("Failed to start RDP: " + ex.Message);
        }
    }

    static IntPtr WaitForWindow(int timeoutMs)
    {
        int elapsed = 0;

        while (elapsed < timeoutMs)
        {
            IntPtr hWnd = FindWindow(null, WINDOW_TITLE);
            if (hWnd != IntPtr.Zero)
            {
                Thread.Sleep(500);
                return hWnd;
            }

            Thread.Sleep(200);
            elapsed += 200;
        }

        return IntPtr.Zero;
    }

    static IntPtr FindChild(IntPtr parent, string text)
    {
        return FindWindowEx(parent, IntPtr.Zero, null, text);
    }

    static bool Click(IntPtr parent, string name)
    {
        IntPtr el = FindChild(parent, name);

        if (el == IntPtr.Zero)
            return false;

        SendMessage(el, BM_CLICK, IntPtr.Zero, IntPtr.Zero);
        return true;
    }

    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Error("Missing RDP file path.");
            return;
        }

        string rdpPath = args[0];

        // 1. launch RDP
        LaunchRdp(rdpPath);

        // 2. wait for window
        IntPtr window = WaitForWindow(10000);

        if (window == IntPtr.Zero)
        {
            Error("RDP window not found.");
            return;
        }

        // 3. optional checkbox clicks (ONLY args after index 0)
        for (int i = 1; i < args.Length; i++)
        {
            if (!Click(window, args[i]))
            {
                Error("Checkbox not found: " + args[i]);
            }

            Thread.Sleep(100);
        }

        // 4. final connect click
        if (!Click(window, CONNECT_BUTTON))
        {
            Error("Connect button not found.");
            return;
        }

        Info("RDP launched successfully.");
    }
}