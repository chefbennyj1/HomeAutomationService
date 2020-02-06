using HomeAutomationService.MagicMirror;
using System.Threading.Tasks;
using HomeAutomationService.Helpers;
using System.Diagnostics;
using HomeAutomationService.Api.Vera;

namespace HomeAutomationService.Vera
{
    public class HomeAutomationEventArgs
    {
       
        /// Z-Wave Mesh Device Info
        public static async Task UpdateZwaveDevicesAsync()
        {

            foreach (var d in VeraApi.ControllerData.devices)
            {
                switch (d.name)
                {
                    
                    case "Fireplace":
                    {
                        if (Equals(d.status, "1") && Equals(Model.DeviceStates["Fireplace"], false))
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

                        if (Equals(d.status, "0")) Model.DeviceStates["Fireplace"] = false;

                        break;
                    }
                                           
                }

                /*
                 TODO: Reinforcement Learning
                 - Make prediction
                 - Set device State
                 - Start timer to wait for user input if prediction is wrong. Timer is 30 seconds
                 - if the device turns back on because of user input edit dataset to change device state record
                 - how long should the device saty on for?
                 - rebuild the model and save it to the data folder for the next time the AI is used
                 * */
            }
        }

       

        //// If HOME Mode check with AI for predicted device status
        //if (VeraApi.HomeAutomationModeState == "1")
        //{ 

        //using (var ai = new RoutineAI())
        //{
        //    var io = ai.PredictDeviceState(d.id.ToString(), DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString());
        //    switch (io)
        //    {
        //        case true:
        //            if (d.status == "0")
        //            {
        //                HttpClient.GET("http://192.168.2.15:3480/data_request?id=action&output_format=xml&DeviceNum=" + d.id.ToString() + "&serviceId=urn:upnp-org:serviceId:SwitchPower1&action=SetTarget&newTargetValue=1");
        //            }
        //            break;
        //        case false:
        //            if (d.status == "1")
        //            {
        //                HttpClient.GET("http://192.168.2.15:3480/data_request?id=action&output_format=xml&DeviceNum=" + d.id.ToString() + "&serviceId=urn:upnp-org:serviceId:SwitchPower1&action=SetTarget&newTargetValue=0");
        //            }
        //            break;
        //    }
        //}



        // }
        //if (Equals(Check, null) || DateTime.Now > Check.AddMinutes(1))
        //{
        //    AutomationJSONDictionary.AppendVeraValues(VeraApi.ControllerData);
        //    Check = DateTime.Now;
        //}




        public static async void UpdateHouseModeStatusDevices()
        {

            if (Equals(VeraApi.HomeAutomationModeState, VeraApi.ControllerData.mode)) return;

            switch (VeraApi.ControllerData.mode)
            {
                case "1":
                    //EmbyMessenger.BroadcastMessageEmbyClients(string.Empty, "Home Automation: Initiating Home Mode");
                    //HttpClient.POST("https://api.notifymyecho.com/v1/NotifyMe?notification=Welcome%20Home%20Anderson%20Family&accessCode=amzn1.ask.account.AEFR4ZCW7HYHZSQN7TWH67SZ4MT3TR5WIPYS6I6T5CBWFLEGOXXBKLJOLZSYAIEGD6ZAD3YT5P6EDGUL3PXWBWKUYU7Q724R5LV5XQ634VXNOONLF46HSEXWB2Q4BUV3E4ZUQITNBA2IA53IJNFUBK3QWEUIBTXH5LEMJ2OMYVCMN45WG7DTEJV6N74PAHLY6BDGQGAQWQCVYUY", "");
                    MagicMirrorCommands.BroadcastMessageMagicMirror("Home Automation: Initiating Home Mode");
                    ConsoleHelper.Write("Vera Mode: Home", ConsoleHelper.AppMessage.Vera);
                               
                    break;

                case "2":
                    //EmbyMessenger.BroadcastMessageEmbyClients(string.Empty, "Home Automation: Initiating Away Mode");
                    //EmbyMessenger.BroadcastMessageEmbyClients(string.Empty, "Home Automation: Initiating Home Mode");
                   MagicMirrorCommands.BroadcastMessageMagicMirror("Home Automation: Initiating Away Mode");
                    ConsoleHelper.Write("Vera Mode: Away", ConsoleHelper.AppMessage.Vera);
                    //if (!Equals(XboxOneApi.XboxOneClient, null)) await XboxOneApi.XboxOneClient.PowerOffAsync();
                                        
                    break;

                   case "3":
                //        EmbyMessenger.BroadcastMessageEmbyClients(string.Empty, "Home Automation: Initiating Night Mode");
                        MagicMirrorCommands.BroadcastMessageMagicMirror("Home Automation: Initiating Night Mode");
                        ConsoleHelper.Write("Vera Mode: Night", ConsoleHelper.AppMessage.Vera);



                        break;
                }
                VeraApi.HomeAutomationModeState = VeraApi.ControllerData.mode;
            

        }
    }
}