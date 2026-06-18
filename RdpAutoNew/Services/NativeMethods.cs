using System;
using System.Runtime.InteropServices;


    public static class NativeMethods
    {
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr ShellExecute(
            IntPtr hwnd,
            string lpOperation,
            string lpFile,
            string lpParameters,
            string lpDirectory,
            int nShowCmd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBoxW(
            IntPtr hWnd,
            string text,
            string caption,
            uint type);

        public const uint MB_OK = 0x00000000;
        public const uint MB_ICONERROR = 0x00000010;
        public const uint MB_ICONWARNING = 0x00000030;
        public const uint MB_ICONINFORMATION = 0x00000040;

        public static void Message(string msg, uint type = MB_ICONERROR)
        {
            MessageBoxW(IntPtr.Zero, msg, "RDP AutoClick", MB_OK | type);
        }
    }
