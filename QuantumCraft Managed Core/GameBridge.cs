using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

using static User32;

namespace QuantumCraft
{
    class GameBridge
    {
        public static Process GetMinecraftProcess()
        {
            Process[] processes = Process.GetProcessesByName("Minecraft.Windows");

            if (processes.Length > 0)
            {
                return processes[0];
            }

            return null;
        }

        public static bool IsFocused
        {
            get
            {
                Process game = GetMinecraftProcess();

                if (game == null)
                    return false;

                var sb = new StringBuilder("Minecraft" + 1);
                GetWindowText(GetForegroundWindow(), sb, "Minecraft".Length + 1);
                return sb.ToString().CompareTo("Minecraft") == 0;
            }
        }

        public static bool CanUseMoveKeys
        {
            get
            {
                if (!IsFocused)
                    return false;

                CURSORINFO cursorInfo = new CURSORINFO();
                cursorInfo.cbSize = Marshal.SizeOf(typeof(CURSORINFO));

                if (GetCursorInfo(out cursorInfo))
                    return (cursorInfo.flags & 0x00000001) == 0;

                return true;
            }
        }
    }
}