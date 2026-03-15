using System;
using System.Runtime.InteropServices;

namespace WpfApp2
{
    public class Windows_API_components
    {
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32.dll")]
        public static extern bool AttachConsole(int pid);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FreeConsole();
    }
}
