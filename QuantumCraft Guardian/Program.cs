using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System;

class Program
{
    private static string minecraftProcessName = "Minecraft.Windows";
    private static NotifyIcon notifyIcon = new NotifyIcon();
    private static Thread monitorThread;
    private static bool gameDetected = false;

    static void Main()
    {
        // Initialize NotifyIcon
        InitializeNotifyIcon();

        // Start monitoring thread
        monitorThread = new Thread(MonitorMinecraft);
        monitorThread.Start();

        // Keep the application running
        Application.Run();
    }

    private static void InitializeNotifyIcon()
    {
        notifyIcon.Icon = System.Drawing.SystemIcons.Application;
        notifyIcon.Visible = true;
        notifyIcon.Text = "QuantumCraft Guardian";
        notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
        ContextMenu contextMenu = new ContextMenu();
        contextMenu.MenuItems.Add("Exit", Exit);
        notifyIcon.ContextMenu = contextMenu;
    }

    private static void NotifyIcon_DoubleClick(object sender, EventArgs e)
    {
        // Handle double-click on the system tray icon
        // Add your logic here
    }

    private static void MonitorMinecraft()
    {
        while (true)
        {
            // Check if Minecraft process is running
            Process minecraftProcess = GameBridge.GetMinecraftProcess();

            if (minecraftProcess != null && !gameDetected)
            {
                // Game detected for the first time, set flag to true
                gameDetected = true;

                // Minecraft is running, get its version from the manifest
                string minecraftVersion = GetMinecraftVersion(minecraftProcess);

                Console.WriteLine($"Detected Minecraft version: {minecraftVersion}");
            }

            // Check if the Minecraft process is not running, reset the flag
            if (minecraftProcess == null)
            {
                gameDetected = false;
            }

            // Adjust the sleep duration based on your monitoring requirements
            Thread.Sleep(500); // Check every half second
        }
    }

    private static string GetMinecraftVersion(Process minecraftProcess)
    {
        string manifestPath = Path.Combine(Path.GetDirectoryName(minecraftProcess.MainModule.FileName), "AppxManifest.xml");

        GameManifest.ManifestPath = manifestPath;

        return GameManifest.GameVersion;
    }

    private static void Exit(object sender, EventArgs e)
    {
        // Clean up resources and exit the application
        notifyIcon.Dispose();
        Environment.Exit(0);
    }
}