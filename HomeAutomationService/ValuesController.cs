using HomeAutomationService.Alexa;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using HomeAutomationService.Api.Alexa;
using HomeAutomationService.Api.FireTV;
using Microsoft.Owin.Security.Provider;

namespace HomeAutomationService
{
    public class ValuesController : ApiController
    {
        private readonly Func<object, bool> isNullOrEmpty = o => o.Equals(null);

        private static readonly Func<object, AlexaRequest> SerializeAlexaRequest = a =>
            JsonConvert.DeserializeObject<AlexaRequest>(JObject.Parse(a.ToString()).ToString());

       
       
        public object Get()
        {
            return "ApiController";
        }

        
        public object Get(int id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);

            switch (id)
            {

                case 1:

                    FireTvController.AdbCommand("shell input keyevent 82");
                    FireTvController.AdbCommand("shell input keyevent 3");
                    return HttpStatusCode.OK;

                case 2:
                    FireTvController.AdbCommand("shell monkey -p  tv.emby.embyatv -c android.intent.category.LAUNCHER 1");
                    return HttpStatusCode.OK;
                
                
                default:

                    return result;
            }
        }

       
        
        

       

        [Route("Alexa/values")]
        public IHttpActionResult Post([FromBody] object value)
        {
            
            AlexaRequest alexaRequest = null;
           
            try { alexaRequest = SerializeAlexaRequest(value); } catch { }

            if (isNullOrEmpty(alexaRequest)) return Ok(new HttpResponseMessage(HttpStatusCode.OK));
            

            if (alexaRequest.request.type == "SessionEndedRequest") return null;

            if (alexaRequest.request.type == "LaunchRequest")
            {
                FireTvController.AdbCommand("shell monkey -p  tv.emby.embyatv -c android.intent.category.LAUNCHER 1");
                return Ok(JObject.Parse(JsonConvert.SerializeObject(new AlexaResponse()
                {
                    version = "1.0",
                    response = new Response()
                    {
                        outputSpeech = new OutputSpeech()
                        {
                            type = "SSML",
                            ssml = "<speak>OK</speak>"
                        },shouldEndSession = true
                    }
                })));
            }

            return null;

        }
        

    }
}

