using System.Collections.Generic;

namespace HomeAutomationService.Alexa 
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ClassNeverInstantiated.Global

    public enum HumanizedResponseType
    {
       
        apologetic = 0,
        
        compliance = 1,
        
        repose = 2
    }

   
    public class AlexaSynthesisResponseLibrary : AlexaApi
    {
        public static string GetResponse(HumanizedResponseType type)
        {
            switch (type.ToString())
            {
                case "compliance":
                    return Compliance[RandomInt.Next(0, Compliance.Count)];
                case "apologetic":
                    return Apologetic[RandomInt.Next(0, Apologetic.Count)];
                case "repose":
                    return Repose[RandomInt.Next(0, Repose.Count)];
            }
            return string.Empty;
        }


        private static readonly List<string> Repose = new List<string>
        {
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "One moment" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "One moment please" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "Just a second" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "This will take just a seck" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "Just a seck" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "Won't take long" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "Be right back" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "I'll be right back" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "Give me a moment" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "Just a moment" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
        };

        private static readonly List<string> Apologetic = new List<string>
        {
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "Sorry about this" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "Apologies" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "I apologize" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "I'm sorry about this" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak)
        };

        private static readonly List<string> Compliance = new List<string>
        {
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "OK" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "Alright" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + AlexaSpeechRate(Rate.slow, "On it!") +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "I'll get on that right away" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "Sure thing!" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "I'll do it" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak) +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "Looking into it now" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak),
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "Yep!" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak) + "Will do!" +
            AlexaInsertStrengthBreak(StrengthBreaks.weak)
        };
    }
}