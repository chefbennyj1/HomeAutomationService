using HomeAutomationService.Helpers;
using System.Diagnostics;

namespace HomeAutomationService.Api.FireTV
{
    public class FireTvController
    {
        public static void AdbCommand(string adbCommand)
        {
        var p = new Process();
        p.StartInfo = new ProcessStartInfo("adb", adbCommand)
        {
            UseShellExecute = false,
            RedirectStandardOutput = true

        };

        p.OutputDataReceived += P_OutputDataReceived;
        p.Start();
        p.WaitForExit();
        p.BeginOutputReadLine();
    }

    private static void P_OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        try
        {
            var s = e.Data;
            if (e.Data != "")
                if (s.Contains("not running"))
                {
                    s = "Starting Android Controller Service";
                }
            ConsoleHelper.StartupLog.Add(s.Replace('*', ' ').Replace("daemon", "Android"), ConsoleHelper.AppMessage.FireTV);

            if (e.Data.Contains("error") || e.Data.Contains("unable") || e.Data.Contains("didn't ACK"))
            {

                Program.RestartApplication();
            }
        }
        catch { }


    }

    public static void KillAdb()
    {
        Process[] processlist = Process.GetProcesses();

        foreach (Process process in processlist)
        {

            if (process.ProcessName.Contains("adb"))
            {
                try
                {
                    process.Kill();
                }
                catch { }
            }
        }
    }

    public static void GetAndroidNetworkDevices()
    {
        AdbCommand("devices");
    }
        public static void LoadFireTvController(string fireTvIp)
        {
            KillAdb();

            AdbCommand("disconnect");
            AdbCommand("kill-server");
            AdbCommand("-P 5038 start-server");
            AdbCommand($"connect {fireTvIp}");

        }

    }
}
