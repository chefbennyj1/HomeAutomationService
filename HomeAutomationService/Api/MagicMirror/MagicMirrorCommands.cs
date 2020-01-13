using System.Net;

namespace HomeAutomationService.MagicMirror
{
    internal class MagicMirrorCommands
    {
        public static void BroadcastMessageMagicMirror(string text)
        {
            string url = "http://192.168.2.48:8080/api/v1/modules/alert/show_ALERT?timer=10500&message=" + text;
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(url);
                request.Method = "POST";
                //request = (HttpWebRequest)WebRequest.Create(url);
                var res = (HttpWebResponse) request.GetResponse();
                // Send Request
                res.GetResponseStream();
            }
            catch
            {
            }
        }
    }
}