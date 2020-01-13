using System.Diagnostics;
using System.Windows.Forms;

namespace HomeAI2.HomeAutomation.Infrared
{
    internal class InfraredController : InfraredCommands
    {
        public static void Send(ProntoDeviceActions deviceAction)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = Application.StartupPath + "\\uutx.exe",
                Arguments =
                    ProntoCodes[deviceAction],
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var irProcess = new Process
            {
                StartInfo = startInfo,
                EnableRaisingEvents = true
            };

            irProcess.Start();


            if (deviceAction == ProntoDeviceActions.FireplaceOff) Model.DeviceStates["Fireplace"] = false;
            if (deviceAction == ProntoDeviceActions.FireplaceOn) Model.DeviceStates["Fireplace"] = true;
            if (deviceAction == ProntoDeviceActions.TVOn) Model.DeviceStates["TV"] = true;
            if (deviceAction == ProntoDeviceActions.TVOff) Model.DeviceStates["TV"] = false;
        }
    }
}