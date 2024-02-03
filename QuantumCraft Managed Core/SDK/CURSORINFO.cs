using System.Runtime.InteropServices;
using System;

namespace QuantumCraft.SDK
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CURSORINFO
    {
        public Int32 cbSize;
        public Int32 flags;
        public IntPtr hCursor;
        public POINTAPI ptScreenPos;
    }
}