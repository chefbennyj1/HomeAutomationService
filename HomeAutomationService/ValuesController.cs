using HomeAutomationService.Alexa;
using HomeAutomationService.Helpers;
using HomeAutomationService.Api.GeoLocation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using HomeAutomationService.Api.FireTV;
using HomeAutomationService.Api.Vera;

namespace HomeAutomationService
{
    public class ValuesController : ApiController
    {
        private readonly Func<object, bool> isNullOrEmpty = o => o.Equals(null);

        private static readonly Func<object, AlexaRequest> SerializeAlexaRequest = a =>
            JsonConvert.DeserializeObject<AlexaRequest>(JObject.Parse(a.ToString()).ToString());

        public object Get()
        {
            return "ApiControlller";
        }

        public object Get(int id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);

            switch (id)
            {

                case 1:

                    FireTVController.adbCommand("shell input keyevent 82");
                    FireTVController.adbCommand("shell input keyevent 3");
                    return HttpStatusCode.OK;

                case 2:
                    FireTVController.adbCommand("shell monkey -p  tv.emby.embyatv -c android.intent.category.LAUNCHER 1");//("shell input keyevent 3");
                    return HttpStatusCode.OK;
                case 3:
                    return ControllerData();
                default:

                    return result;
            }
        }
        
        public string ControllerData()
        {
            try
            {
                return Helpers.HttpClient.GET("http://192.168.2.15:3480/data_request?id=sdata&output_format=json");
            }
            catch { }

            return string.Empty;
        }

        [Route("Alexa/values")]
        public async Task<IHttpActionResult> Post([FromBody] object value)
        {
            AlexaRequest alexaRequest = null;

            try { alexaRequest = SerializeAlexaRequest(value); } catch { }

            if (isNullOrEmpty(alexaRequest)) return Ok(new HttpResponseMessage(HttpStatusCode.OK));

            var alexa = new AlexaApi();

            if (alexa.LaunchRequest(alexaRequest) ||
                Equals(alexa.RequestName(alexaRequest), "LatestPrices"))  // asking to "turn on" 
            {
                var city = "Peterborough";
                var prov = "Ontario";

                AmazonReverseGeolocationResponse infos = null;

                if (alexaRequest.context.Geolocation != null)
                {
                    infos = GeoLocator.GetLocation(alexaRequest);
                    city = infos.major.city;
                    prov = infos.major.prov;
                }

                alexa.PostDirectiveResponseAsync(alexaRequest, string.Format("Just a moment...",
                    AlexaApi.AlexaInsertStrengthBreak(AlexaApi.StrengthBreaks.weak)));


                //var stationsResult = await Task.WhenAny(GetStationData(prov, city), TimeoutTask());

                //return Ok(AlexaApi.ResponseBuilder(stationsResult.Result));

            }



            //await Task.Delay(8000);
            //alexa.PostDirectiveResponseAsync(alexaRequest, string.Format("Collecting Prices. {0}",
            //   AlexaApi.AlexaInsertStrengthBreak(AlexaApi.StrengthBreaks.weak)));
            //await Task.Delay(8000);

            //return Ok(AlexaApi.ResponseBuilder("Sorry I was unable to get gas prices"));


            if (alexa.CanFulfillIntentRequest(alexaRequest))
            {
                Console.WriteLine(DateTime.Now + ":   CanFulfillIntentRequest ");
            }

            if (!alexa.SessionEndedRequest(alexaRequest))
            {
                switch (alexa.RequestName(alexaRequest))
                {
                    case "AMAZON.DeactivateAction<object@Thing>":
                    case "TurnOff":
                        
                                      

                    case "AMAZON.YesIntent":
                    case "AMAZON.NoIntent":

                        return Ok(AlexaApi.ResponseBuilder("OK.", true));

                        //case "OpenComputerApp":

                        //case "ComputerLabStartup":
                        //    Factory.HttpClient.GET(
                        //        "http://192.168.2.15:3480/data_request?id=action&output_format=xml&DeviceNum=117&serviceId=urn:upnp-org:serviceId:SwitchPower1&action=SetTarget&newTargetValue=1");
                        //    return AlexaApi.ResponseBuilder(string.Format("{0} Starting up Computer Lab. {1} What are we doing today sir?", 
                        //        AlexaSynthesisResponseLibrary.GetResponse(HumanizedResponseType.compliance), 
                        //        AlexaApi.AlexaInsertStrengthBreak(AlexaApi.StrengthBreaks.weak)), false);


                }
            }
            return Ok(new HttpResponseMessage(HttpStatusCode.OK));
        }

        private async Task<string> TimeoutTask()
        {
            await Task.Delay(10000);
            return "Sorry I couldn't access this Data at the moment.";
           
        }
        /*
        private async Task<string> GetStationData(string prov, string city)
        {
            var stations = await Task.Factory.StartNew(() => GasPriceApi.GetGasStations(prov, city));
            if (!Equals(stations, null))
            {
                var speak = "Here are the top gas stations near you in " + city + " with the best prices... ";
                foreach (var item in stations)
                {
                    //var location = item.Location.Split(' ')[1] + item.Location.Split(' ')[2];
                    speak += item.Name + "... at " + item.Location + " has gas for " + item.Price.Replace("¢", "") + " cents per liter, ";
                }
                return (speak);
            }
            else
            {
                return ("Sorry there is no gas station information available for " + city);
            }
        }
        */

    }
}

