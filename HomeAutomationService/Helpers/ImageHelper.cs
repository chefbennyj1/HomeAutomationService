using System;
using System.Net;
using System.Drawing;

namespace HomeAutomationService.Helpers
{
    public class ImageHelper
    {
        public static void GetCameraImageFromUrl(string imageUrl)
        {
            Image image = null;

            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(imageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;
                webRequest.Credentials = new NetworkCredential()
                {
                    UserName = "admin",
                    Password = "admin"
                };
                WebResponse webResponse = webRequest.GetResponse();

                var stream = webResponse.GetResponseStream();

                image = Image.FromStream(stream);

                image.Save("image_source.jpg");
                image.Dispose();
                webResponse.Close();
            }
            catch (Exception ex)
            {
               
            }           
        }

      
    }
}
