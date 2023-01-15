using System;
using Microsoft.Owin.Hosting;
using HomeAutomationService.Helpers;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using HomeAutomationService.Api.FireTV;


namespace HomeAutomationService
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {      
       
        

        public static Timer T { get; set; }
        
        [STAThread]
        private static void Main()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            Console.Write(ConsoleHelper.CreateConsoleHeader());
            ConsoleHelper.Write("Loading...", ConsoleHelper.AppMessage.Status);
            T = new Timer(RestartApplication);
            T.Change(500, 500);

            string fireTvIp = string.Empty;
            string serviceIp = string.Empty;

            AppDomain.CurrentDomain.DomainUnload += new EventHandler(ExitApplication);

            string hostName = Dns.GetHostName();
            IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipAddresses)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    serviceIp = (ip.ToString());
                }
            }

            ConsoleHelper.Write($"Starting Service: {serviceIp}:9925", ConsoleHelper.AppMessage.Status);
            
            Console.WriteLine("\n Please turn on your fire Tv now...");

            if (!File.Exists("fireTv.txt"))
            {
                Console.WriteLine("\n\nPlease enter the Ip address of the FireTV you want to control:");
                fireTvIp = Console.ReadLine();
                SaveFireTV(fireTvIp);
            }
            else
            {
                fireTvIp = File.ReadAllText("fireTv.txt");
            }

            ConsoleHelper.Write("Loading...", ConsoleHelper.AppMessage.FireTV);

            FireTvController.LoadFireTvController(fireTvIp);

            //Alexa Endpoint Service
            string baseAddress = $"http://{serviceIp}:9925/";
            using (WebApp.Start<Startup>(baseAddress))
            {
                
                ConsoleHelper.StartupLog.Add("Amazon Alexa Fire TV Endpoint Connected", ConsoleHelper.AppMessage.Alexa);
                
                ConsoleHelper.Write("FireTv Ready...", ConsoleHelper.AppMessage.FireTV);

                Console.WriteLine("\n Skill endpoint should point to: Alexa/values");

                

                //Have to add all these messages for boot up to a dictionary because they are all run spiratically and they mess up the console and interfere with each other.
                foreach (KeyValuePair<string, ConsoleHelper.AppMessage> entry in ConsoleHelper.StartupLog)
                {
                    ConsoleHelper.Write(entry.Key, entry.Value);
                }

                Console.CursorTop = 5;
                ConsoleHelper.Write("Ready.", ConsoleHelper.AppMessage.Status);
                Console.CursorTop = 21;
                Console.ReadLine();
            }

        }

        public static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            ConsoleHelper.Write("Goodbye", ConsoleHelper.AppMessage.Status);
        }

        public static void SaveFireTV(string ip)
        {
            File.WriteAllText("fireTv.txt", ip);
        }
        public static void RestartApplication(object sender = null)
        {           
            var now = DateTime.Now;
            var scheduledTime = new DateTime(now.Year, now.Month, now.Day, 1, 40,0);
            if (now.Hour == 23 && now.Minute == 45 && now.Second <= 10)
            {
                T.Change(Timeout.Infinite, Timeout.Infinite);
                FireTvController.KillAdb();
                // Get file path of current process 
                var filePath = Assembly.GetExecutingAssembly().Location;

                // Start program
                Process.Start(filePath);

                // Closes the current process
                Environment.Exit(0);
            }

            if (now.Hour == 16 && now.Minute == 45 && now.Second <= 10)
            {
                T.Change(Timeout.Infinite, Timeout.Infinite);
                FireTvController.KillAdb();
                // Get file path of current process 
                var filePath = Assembly.GetExecutingAssembly().Location;

                // Start program
                Process.Start(filePath);

                // Closes the current process
                Environment.Exit(0);
            }
        }


        private static void ExitApplication(object sender, EventArgs e)
        {           
            
            Environment.Exit(0);
        }
    }
}