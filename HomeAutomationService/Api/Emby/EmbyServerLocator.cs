

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using HomeAutomationService.Helpers;


namespace HomeAutomationService.Emby
{
    public class EmbyServerLocator
    {
        public static ServerInfo ServerProtocolAddress()
        {
            // :: Create a udp client
            var udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, GetRandomUnusedPort()))
            {
                Client = {ReceiveTimeout = 24000}
            };
            // :: Construct the message the server is expecting
            byte[] bytes = Encoding.UTF8.GetBytes("who is EmbyServer?");
            // :: Send it - must be IPAddress.Broadcast, 7359
            var targetEndPoint = new IPEndPoint(IPAddress.Broadcast, 7359);
            // :: Send it
            udpClient.Send(bytes, bytes.Length, targetEndPoint);
            // :: Get a result back

            try
            {
                byte[] result = udpClient.Receive(ref targetEndPoint);
                // :: Convert bytes to text
                string json = Encoding.UTF8.GetString(result);

                var info = new NewtonsoftJsonSerializer().DeserializeFromString<ServerInfo>(json);

                return new ServerInfo
                {
                    Id = info.Id,
                    Name = info.Name,
                    Address = info.Address
                };
            }
            catch (Exception)
            {
                //Console.WriteLine(ex.Message);
            }
            return null;
        }

        /// <summary>
        ///     Gets a random port number that is currently available
        /// </summary>
        private static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Any, 0);
            listener.Start();
            int port = ((IPEndPoint) listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }
    }

    public class ServerInfo
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Address { get; set; }
    }
}