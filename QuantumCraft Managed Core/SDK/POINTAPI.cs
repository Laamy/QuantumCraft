using System.Runtime.InteropServices;

namespace QuantumCraft.SDK
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINTAPI
    {
        public int x;
        public int y;
    }
}