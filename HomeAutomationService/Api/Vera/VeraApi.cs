using System.Threading;
using HomeAutomationService.Helpers;
using HomeAutomationService.Vera;

namespace HomeAutomationService.Api.Vera
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedAutoPropertyAccessor.Global

    internal class VeraApi
    {
        public static VeraController ControllerData { get; private set; }
        private static Timer StateMonitor { get; set; }
        public static string HomeAutomationModeState = "0";
       
        public static void BuildVeraClient(bool CreateStateMonitor)
        {
            if (CreateStateMonitor)
            {
                StateMonitor = new Timer(VeraStateMonitorDelegate);
                StateMonitor.Change(1200, 1000);
                ConsoleHelper.StartupLog.Add("Vera Home Automation Connected", ConsoleHelper.AppMessage.Vera);
            }
        }

        private static void VeraStateMonitorDelegate(object sender)
        {
            StateMonitor.Change(Timeout.Infinite, Timeout.Infinite); 
            
            var json = string.Empty;
            try
            {
                json = HttpClient.GET("http://192.168.2.15:3480/data_request?id=sdata&output_format=json");
            }
            catch  { }

            if (Equals(json, string.Empty)) return;

            try
            {
                ControllerData = new NewtonsoftJsonSerializer().DeserializeFromString<VeraController>(json);
            }
            catch { }

            HomeAutomationEventArgs.UpdateZwaveDevicesAsync();
            HomeAutomationEventArgs.UpdateHouseModeStatusDevices();

            

            StateMonitor.Change(1000, 1000);
        }
    }
}