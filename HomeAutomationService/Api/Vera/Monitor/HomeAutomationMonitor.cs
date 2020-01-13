using System.Threading;
using HomeAI2.Emby;
using HomeAI2.Factory;
using HomeAI2.HomeAutomation.Api.Controller;
using HomeAI2.HomeAutomation.Api.ZwaveDevices;
using HomeAI2.HomeAutomation.EventArgs;

namespace HomeAI2.HomeAutomation.Monitor
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedAutoPropertyAccessor.Global

    internal class HomeAutomationMonitor
    {
        public static void HomeAutomationMonitorDelegate(object sender)
        {
            Program.HomeAutomationWatcher.Change(Timeout.Infinite, Timeout.Infinite);


            //GeoFencing
            string completeJsonData = string.Empty;

            try
            {
                completeJsonData =
                    HttpClient.RequestJson("http://192.168.2.15:3480/data_request?id=user_data&output_format=json");
            }
            catch
            {
            }

            if (completeJsonData != string.Empty)
            {
                Model.CData =
                    new NewtonsoftJsonSerializer().DeserializeFromString<HomeAutomationControllerDto>(completeJsonData);
            }

            // Device Info
            string deviceJsonData = string.Empty;
            try
            {
                deviceJsonData =
                    HttpClient.RequestJson("http://192.168.2.15:3480/data_request?id=sdata&output_format=json");
            }
            catch
            {
            }


            if (deviceJsonData != string.Empty)
            {
                Model.Info =
                    new NewtonsoftJsonSerializer().DeserializeFromString<ZwaveHomeAutomationDeviceDto>(deviceJsonData);

                HomeAutomationEventArgs.UpdateIrDevices();
                HomeAutomationEventArgs.UpdateHouseModeStatusDevices();
                HomeAutomationEventArgs.UpdateXboxOne();
               
                //EmbySessionRss.UpdateSessionRssData();
            }


            Program.HomeAutomationWatcher.Change(1000, 4000);
        }
    }
}