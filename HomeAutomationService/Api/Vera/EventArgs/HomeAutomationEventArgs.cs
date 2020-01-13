using System.Linq;
using HomeAI2.Emby;
using HomeAI2.HomeAutomation.Api.ZwaveDevices;
using HomeAI2.MagicMirror;
using HomeAI2.XboxOne;

namespace HomeAI2.HomeAutomation.EventArgs
{
    // ReSharper disable MaximumChainedReferences
    // ReSharper disable ClassNeverInstantiated.Global
    public class HomeAutomationEventArgs
    {
        /// Z-Wave Mesh Device Info
        public static void UpdateIrDevices()
        {
            #region Turn on Firplace if fireplace is on

            //
            //Turns on fireplace if the Z wave plug is switch on 
            //

            foreach (Device d in Model.Info.devices.Where(d => d.name == "Fireplace"))
            {
                if (d.status == "1" && Model.DeviceStates["Fireplace"] == false)
                {
                    try
                    {
                        Program.ControlFireplace.RunWorkerAsync();

                        Model.DeviceStates["Fireplace"] = true;
                    }
                    catch
                    {
                    }
                }

                if (d.status == "0")
                {
                    Model.DeviceStates["Fireplace"] = false;
                }
            }

            #endregion

            #region Turn on TV if TV is on and off if off

            foreach (Device d in Model.Info.devices.Where(d => d.name == "TV"))
            {
                if (d.status == "1" && Model.DeviceStates["TV"] == false)
                {
                    try
                    {
                        Program.ControlTVOn.RunWorkerAsync();

                        Model.DeviceStates["TV"] = true;
                    }
                    catch
                    {
                    }
                }

                // ReSharper disable once RedundantBoolCompare
                if (d.status == "0" && Model.DeviceStates["TV"] == true)
                {
                    try
                    {
                        Program.ControlTVOff.RunWorkerAsync();

                        Model.DeviceStates["TV"] = false;
                    }
                    catch
                    {
                    }
                }
            }

            #endregion

           
        }

        public static void UpdateXboxOne()
        {
            
            #region Turn on XBOX and off XBOX
            foreach (Device d in Model.Info.devices.Where(d => d.name == "XBOX_CONSOLE"))
            {
                if (d.status == "1" && Model.DeviceStates["XBOX_CONSOLE"] == false)
                {
                    try
                    {
                        XboxOneController.TurnOnXbox();

                        Model.DeviceStates["XBOX_CONSOLE"] = true;
                    }
                    catch
                    {
                    }
                }

                // ReSharper disable once RedundantBoolCompare
                if (d.status == "0" && Model.DeviceStates["XBOX_CONSOLE"] == true)
                {
                    try
                    {
                        XboxOneController.TurnOffXbox();

                        Model.DeviceStates["XBOX_CONSOLE"] = false;
                    }
                    catch
                    {
                    }
                }
            }
            #endregion
        }

        public static void UpdateHouseModeStatusDevices()
        {
            if (Model.HomeAutomationModeState == Model.CData.Mode) return;
            switch (Model.CData.Mode)
            {
                case "1":
                    EmbyCommands.BroadcastMessageEmbyClients(string.Empty, "Home Automation: Initiating Home Mode");
                    MagicMirrorCommands.BroadcastMessageMagicMirror("Home Automation: Initiating Home Mode");
                    break;
                case "2":
                    EmbyCommands.BroadcastMessageEmbyClients(string.Empty, "Home Automation: Initiating Away Mode");
                    MagicMirrorCommands.BroadcastMessageMagicMirror("Home Automation: Initiating Away Mode");
                    break;
                case "3":
                    EmbyCommands.BroadcastMessageEmbyClients(string.Empty, "Home Automation: Initiating Night Mode");
                    MagicMirrorCommands.BroadcastMessageMagicMirror("Home Automation: Initiating Night Mode");
                    break;
            }
            Model.HomeAutomationModeState = Model.CData.Mode;
        }
    }
}