using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomeAutomationService.Alexa 
{
   
    /// <summary>
    ///     C# wrapper for AlexaApi Custom Skills
    /// </summary>
    public class AlexaApi 
    {
        protected static readonly Random RandomInt = new Random();
        
        //Speech Synthesis
        public enum Effect
        {
            whispered = 0
        }

        public enum Emphasis
        {
            // ReSharper disable once InconsistentNaming
            /// <summary>
            ///     Decrease the volume and speed up the speaking rate. The speech is softer and faster.
            /// </summary>
            reduced = 0,
            // ReSharper disable once InconsistentNaming
            /// <summary>
            ///     Increase the volume and slow down the speaking rate so the speech is louder and slower.
            /// </summary>
            strong = 1,

            // ReSharper disable once InconsistentNaming
            /// <summary>
            ///     Increase the volume and slow down the speaking rate, but not as much as when set to strong. This is used as a
            ///     default if level is not provided.
            /// </summary>
            moderate = 2
        }

        public enum Rate
        {
            // ReSharper disable once InconsistentNaming
            slow = 0,
            // ReSharper disable once InconsistentNaming
            medium = 1,
            // ReSharper disable once InconsistentNaming
            fast = 2
        }

        public enum StrengthBreaks
        {
            // ReSharper disable once InconsistentNaming
            weak = 0,
            // ReSharper disable once InconsistentNaming
            medium = 1,
            // ReSharper disable once InconsistentNaming
            strong = 2
        }

        public static Func<Effect, string, string> AlexaSayWithEffect = (effect, text) =>
            string.Format("<amazon:effect name=\"{0}\">{1}</amazon:effect>", effect, text);

        public static Func<string, string> AlexaSayAsCardinal = text =>
            string.Format("<say-as interpret-as=\"cardinal\">{0}</say-as>", text);

        public static Func<string, string> AlexaSpellOut = text =>
            string.Format("<say-as interpret-as=\"spell-out\">" + text + "</say-as>.");

        public Func<string, string> AlexaInsertTimedBreak = intDurationSeconds =>
            string.Format("<break time=\"{0}s\"/>", intDurationSeconds);

        public static readonly Func<StrengthBreaks, string> AlexaInsertStrengthBreak = s =>
            string.Format("<break strength=\"{0}\"/>", s);

        public static readonly Func<string, Emphasis, string> AlexaEmphasis = (text, emphasis) =>
            string.Format("<emphasis level=\"{0}\">{1}</emphasis>", emphasis, text);

        public static readonly Func<Rate, string, string> AlexaSpeechRate = (rate, text) =>
            string.Format("<prosody rate=\"{0}\">{1}</prosody>", rate, text);

        public static readonly Func<string, string> AlexaExpressiveInterjection = text =>
            string.Format("<say-as interpret-as=\"interjection\">{0}</say-as>", text);
        
        //Request Info

        public readonly Func<AlexaRequest, string> RequestName = r => r.request.intent.name;
        public readonly Func<AlexaRequest, string> ApiAccessToken = r => r.context.System.apiAccessToken;
        public readonly Func<AlexaRequest, string> RequestId = r => r.request.requestId;
        public readonly Func<AlexaRequest, bool> LaunchRequest = r => r.request.type.Equals("LaunchRequest");
        public readonly Func<AlexaRequest, bool> SessionEndedRequest = r => r.request.type.Equals("SessionEndedRequest");
        public readonly Func<AlexaRequest, bool> CanFulfillIntentRequest = r => r.request.type.Equals("CanFulfillIntentRequest");
        public readonly Func<AlexaRequest, DateTime> TimeStamp = r => r.request.timestamp;
               
        
        //Builders
        public static object ResponseBuilder(string text, bool SessionEndedRequest = true, Card displayCard = null, string audioUrl = "")
        {
            var speech = "";
            if(audioUrl == "")
            {
                speech = "<speak>" + text + "</speak>";
            } else
            {
                speech = "<speak>" + text + "  < audio src = '" + audioUrl + " /></speak>";
            }
            var alexaResponse = new AlexaResponse
            {
                version = "1.0",
                response = new Response
                {
                    outputSpeech = new OutputSpeech
                    {
                        type = "SSML",
                        ssml = "<speak>" +
                               text + "</speak>"
                    },
                    shouldEndSession = SessionEndedRequest,
                    //card = displayCard
                }
            };

            //Convert the JSON string to an actual JSON Object
            return JObject.Parse(JsonConvert.SerializeObject(alexaResponse));
        }

        private DirectiveResponse DirectiveResponseBuilder(string alexaRequestId, string content, string audioUrl = "")
        {
            return new DirectiveResponse
            {
                header = new Header {requestId = alexaRequestId},
                directive = new Directive
                {
                    type = "VoicePlayer.Speak",
                    speech = "<speak>" + content +
                             "<audio src='" + audioUrl + "'/></speak>"
                }
            };
        }

        public Card CardBuilder(Image imageUrl, string content, string title, string text, string type = "Standard")
        {
            return new Card
            {
                content = content,
                image = imageUrl,
                text = text,
                title = title,
                type = type
            };
        }
        
        public void PostDirectiveResponseAsync(AlexaRequest alexaRequest, string text)
        {
            const string uri = "https://api.amazonalexa.com/v1/directives";
            var client = new HttpClient();
            
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ApiAccessToken(alexaRequest));

            var content =
                new ByteArrayContent(
                    Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(
                        DirectiveResponseBuilder(RequestId(alexaRequest), text,  Sounds.GetRandomSound()))));
                
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    Task.Run(() => client.PostAsync(uri, content));
                
            
        }
        
        public class Sounds
        {
            public static string GetRandomSound()
            {
                return SoundList[RandomInt.Next(0, SoundList.Count)];
            }

            public static readonly List<string> SoundList = new List<string>
            {
                "https://s3.amazonaws.com/ask-soundlibrary/ui/gameshow/amzn_ui_sfx_gameshow_positive_response_01.mp3",
                //"https://s3.amazonaws.com/ask-soundlibrary/ui/gameshow/amzn_ui_sfx_gameshow_bridge_01.mp3",
                "https://s3.amazonaws.com/ask-soundlibrary/ui/gameshow/amzn_ui_sfx_gameshow_neutral_response_03.mp3"
            };
        }
      
    }
}