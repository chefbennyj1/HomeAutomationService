using System;
using System.Text;
using Console = Colorful.Console;
using System.Drawing;
using System.Collections.Generic;

namespace HomeAutomationService.Helpers
{
    public class ConsoleHelper
    {
        public static Dictionary<string, AppMessage> StartupLog = new Dictionary<string, AppMessage>();

        public enum AppMessage
        {
            Emby = 0,
            Alexa = 1,
            Netflix = 2,
            Error = 3,
            Status = 4,
            Vera = 5,
            Xbox = 6,
            FireTV = 7
        }
        public static void Write(string text, AppMessage appMessage)
        {
            var c = GetAppColor(appMessage);
                                  
            Console.Write(string.Format("{0, 2}", Encoding.Default.GetString(new byte[] { 0172 })), c);
            Console.Write(string.Format("{0, 22}", DateTime.Now, Color.White));      
            Console.Write( string.Format("{0, 8}", Enum.GetName(typeof(AppMessage), appMessage)), c);
            Console.Write(string.Format("{0, 3}",  Encoding.Default.GetString(new byte[] { 0187 })), c);
            Console.Write(string.Format("{0, 43}\n", text), Color.White);
            Console.WriteLine();
        }

        public static string CreateConsoleHeader()
        {
            var header = string.Empty;
            header += "\n\n                          HOME AUTOMATION CONTROL              \n";
            header += "        ___________________________________________________________\n\n";
            return header;
        }

        private static Color GetAppColor(AppMessage appMessage)
        {
            switch (appMessage)
            {
                case AppMessage.Emby:
                    return Color.FromArgb(82, 181, 75);
                case AppMessage.Alexa:
                    return Color.FromArgb(49, 196, 243);
                case AppMessage.Netflix:
                    return Color.Red;
                case AppMessage.Error:
                    return Color.DarkRed;
                case AppMessage.Vera:
                    return Color.SeaGreen;
                case AppMessage.FireTV:
                    return Color.Purple;
            }
            return Color.Magenta;
        }
       
    }

   
}
