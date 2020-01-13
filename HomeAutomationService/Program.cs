using System;
using System.ComponentModel;
using HomeAutomationService.Emby;
using HomeAutomationService.Vera;

using Microsoft.Owin.Hosting;
using HomeAutomationService.Helpers;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using HomeAutomationService.Api.FireTV;

namespace HomeAutomationService
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {      
       
        public static BackgroundWorker ControlFireplace { get; private set; }

        public static Timer T { get; set; }
        
        [STAThread]
        private static void Main()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            Console.Write(ConsoleHelper.CreateConsoleHeader());
            ConsoleHelper.Write("Loading...", ConsoleHelper.AppMessage.Status);
            T = new Timer(RestartApplication);
            T.Change(500, 500);

            

            AppDomain.CurrentDomain.DomainUnload += new EventHandler(ExitApplication);


            FireTVController.LoadFireTVController();
            
            //Vera Home Automation Service
            VeraApi.BuildVeraClient(CreateStateMonitor: true);

            // Fireplace Controller
            ControlFireplace = new BackgroundWorker();
            ControlFireplace.DoWork += InfraredEvents.FireplaceOn;

            //Alexa Endpoint Service
            const string baseAddress = "http://192.168.2.104:9925/";
            using (WebApp.Start<Startup>(baseAddress))
            {

                //Initiate Machine Learning - Experimental
                //var io = new RoutineAI();
               //Editor.EditDataset(RoutineAI.LoadTrainningData());


                ConsoleHelper.StartupLog.Add("Amazon Alexa Endpoint Connected", ConsoleHelper.AppMessage.Alexa);


                //Have to add all these messages for boot up to a dictionary because they are all run spiratically and they mess up the console and interfesr with each other.
                foreach (KeyValuePair<string, ConsoleHelper.AppMessage> entry in ConsoleHelper.StartupLog)
                {
                    ConsoleHelper.Write(entry.Key, entry.Value);
                }

                Console.ReadLine();
            }

            



        }

        public static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            ConsoleHelper.Write("Goodbye", ConsoleHelper.AppMessage.Status);
        }

        public static void RestartApplication(object sender = null)
        {           
            var now = DateTime.Now;
            var scheduledTime = new DateTime(now.Year, now.Month, now.Day, 1, 40,0);
            if (now.Hour == 23 && now.Minute == 45 && now.Second <= 10)
            {
                T.Change(Timeout.Infinite, Timeout.Infinite);
                FireTVController.KillADB();
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
                FireTVController.KillADB();
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