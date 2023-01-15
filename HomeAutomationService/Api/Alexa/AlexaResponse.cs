using System;
using System.Collections.Generic;
using HomeAutomationService.Alexa;

namespace HomeAutomationService.Api.Alexa 
{
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // ReSharper disable UnusedMember.Global

   
    [Serializable]
    public class AlexaResponse
    {
        /// <summary>
        ///     Mandatory
        /// </summary>
        public string version { get; set; }

        /// <summary>
        ///     Not Mandatory
        /// </summary>
        public SessionAttributes sessionAttributes { get; set; }

        /// <summary>
        ///     Mandatory
        /// </summary>
        public Response response { get; set; }
    }

    [Serializable]
    public class Card
    {
        public string type { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string text { get; set; }
        public Image image { get; set; }
    }
    
    [Serializable]
    public class Image
    {
        public string smallImageUrl { get; set; }
        public string largeImageUrl { get; set; }
    }
    
    [Serializable]
    public class OutputSpeech
    {
        public string type { get; set; }
        public string text { get; set; }
        public string ssml { get; set; }
    }
    
    [Serializable]
    public class OutputSpeech2
    {
        public string type { get; set; }
        public string text { get; set; }
        public string ssml { get; set; }
    }

    [Serializable]
    public class Reprompt
    {
        public OutputSpeech2 outputSpeech { get; set; }
    }

    public class Directive
    {
        public string type { get; set; }
        public string speech { get; set; }

        public string token { get; set; }
        public List<object> commands { get; set; }
    }

    [Serializable]
    public class Response
    {
        public OutputSpeech outputSpeech { get; set; }
        public Card card { get; set; }
        public Reprompt reprompt { get; set; }
        public object shouldEndSession { get; set; }
        public List<Directive> directives { get; set; }
    }

    [Serializable]
    public abstract class SessionAttributes
    {
        public string key { get; set; }
    }
}

