using System.Runtime.InteropServices;
using System.Text;
using System;

namespace QuantumCraft.SDK
{
    internal class User32
    {
        [DllImport("User32.dll")] public static extern bool GetCursorInfo(out CURSORINFO pci);
        [DllImport("User32.dll")] public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        [DllImport("User32.dll")] public static extern IntPtr GetForegroundWindow();
    }
}