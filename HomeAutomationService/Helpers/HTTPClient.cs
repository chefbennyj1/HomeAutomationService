using System.IO;
using System.Net;
using System.Text;

namespace HomeAutomationService.Helpers
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedAutoPropertyAccessor.Global

    internal class HttpClient
    {
        public static string GET(string http)
        {
            var json = string.Empty;
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(http);
                request.Method = "GET";
                //request = (HttpWebRequest) WebRequest.Create(http);
                var res = (HttpWebResponse) request.GetResponse();
                // Send Request
                var webStream = res.GetResponseStream();
                // Get Response
                if (webStream == null) return string.Empty;
                var webStreamReader = new StreamReader(webStream);
                json = webStreamReader.ReadToEnd();
            }
            catch
            {
            }
            return json;
        }

        public static string POST(string http, string message)
        {
           
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(http);
                request.Method = "POST";
                request.ContentType = "application/json";
                byte[] data = Encoding.ASCII.GetBytes(message);
                request.ContentLength = data.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();
                Stream responseStream = myHttpWebResponse.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);
                string pageContent = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                responseStream.Close();
                myHttpWebResponse.Close();
                return pageContent; 

            }
            catch
            {
            }
            return null;
        }
    }
}