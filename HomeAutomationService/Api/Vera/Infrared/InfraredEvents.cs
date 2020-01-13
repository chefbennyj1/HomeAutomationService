using System;
using System.ComponentModel;
using System.Threading;

namespace HomeAI2.HomeAutomation.Infrared
{
    internal class InfraredEvents
    {
        public static void TVOn(object sender, DoWorkEventArgs e)
        {
            try
            {
                Thread.Sleep(2000);
                InfraredController.Send(InfraredCommands.ProntoDeviceActions.TVOn);
                Model.DeviceStates["TV"] = true;
            }
            catch
            {
            }
        }

        public static void TVOff(object sender, DoWorkEventArgs e)
        {
            try
            {
                Thread.Sleep(2000);
                InfraredController.Send(InfraredCommands.ProntoDeviceActions.TVOff);
                Model.DeviceStates["TV"] = false;
            }
            catch
            {
            }
        }

        public static void FireplaceOn(object sender, DoWorkEventArgs e)
        {
            try
            {
                Thread.Sleep(2000);
                InfraredController.Send(InfraredCommands.ProntoDeviceActions.FireplaceOff);
                Thread.Sleep(3000);
                InfraredController.Send(InfraredCommands.ProntoDeviceActions.FireplaceOn);
                Thread.Sleep(5000);
                InfraredController.Send(InfraredCommands.ProntoDeviceActions.FireplaceOn);
                Thread.Sleep(5000);
                InfraredController.Send(InfraredCommands.ProntoDeviceActions.FireplaceOn);
                Thread.Sleep(5000);
                InfraredController.Send(InfraredCommands.ProntoDeviceActions.FireplaceOn);

                Model.DeviceStates["Fireplace"] = true;
            }
            catch (Exception)
            {
            }
        }
    }
}