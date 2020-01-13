using HomeAutomationService.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationService.MachineLearning
{
    public class Editor
    {
        public static void EditDataset(List<AutomationTrainingData> data)
        {
            var fixedList = new List<AutomationTrainingData>();
            foreach(var d in data)
            {
                var newItem = new AutomationTrainingData();
                newItem.DeviceDate = d.DeviceDate.Replace("Sunday", string.Empty).Replace("Monday", string.Empty).Replace("Tuesday", string.Empty).Replace("Wednesday", string.Empty).Replace("Thursday", string.Empty).Replace("Friday", string.Empty).Replace("Saturday", string.Empty);
                newItem.State = d.State;
                    //for (var i = 2000; i <= 2459; i++)
                //{
                //    if (d.DeviceDate.Equals("18-" + i)) 
                //    {
                //        d.State = false;
                //    }
                //}

                fixedList.Add(newItem);
            }
            Console.WriteLine("Saving...");
            var json = new NewtonsoftJsonSerializer().SerializeToString(fixedList);
            using (var sw = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "MachineLearning/Data/", "dataFIXED.json")))
            {
                sw.Write(json);
            }
            Console.WriteLine("Complete");
        }
    }
}
