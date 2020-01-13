using HomeAutomationService.Vera;
using HomeAutomationService.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationService.MachineLearning
{
    public class AutomationJSONDictionary
    {

        public static  string dataDir = Path.Combine(Environment.CurrentDirectory, "MachineLearning/Data/", "data.json");

        public static void AppendXboxValues(bool state)
        {
            EnsureDirectoryExists(dataDir);
            var dataList = new List<AutomationTrainingData>();

            try
            {
                using (var sr = new StreamReader(dataDir))
                {
                    dataList = new NewtonsoftJsonSerializer().DeserializeFromString<List<AutomationTrainingData>>(sr.ReadToEnd());
                }
            }
            catch (Exception)
            { }


            var data = new AutomationTrainingData
            {               
                DeviceDate = string.Format("{0}-{1}", "999",  DateTime.Now.ToString("HHmm")),
                State = state
            };

            dataList.Add(data);
              
            var json = new NewtonsoftJsonSerializer().SerializeToString(dataList);
            using (var sw = new StreamWriter(dataDir))
            {
                sw.Write(json);
            }
        }
        static Func<string, bool> StateBoolean = x => x == "1";
        public static void AppendVeraValues(VeraController entry)
        {
            EnsureDirectoryExists(dataDir);
            var dataList = new List<AutomationTrainingData>();
           
            try
            {
                using (var sr = new StreamReader(dataDir))
                {
                    dataList = new NewtonsoftJsonSerializer().DeserializeFromString<List<AutomationTrainingData>>(sr.ReadToEnd());
                }
            } catch (Exception)
            { }

            for (int i = 0; i < entry.devices.Count; i++)
            {
                if (entry.devices[i].status == "1" || entry.devices[i].status == "0")
                {
                    var data = new AutomationTrainingData
                    {
                        DeviceDate = string.Format("{0}-{1}",  entry.devices[i].id.ToString(), DateTime.Now.ToString("HHmm")),
                        State = StateBoolean(entry.devices[i].status)
                    };
                    dataList.Add(data);
                }
            }

            var json = new NewtonsoftJsonSerializer().SerializeToString(dataList);
            using (var sw = new StreamWriter(dataDir))
            {
                sw.Write(json);
            }
        }
        private static bool EnsureDirectoryExists(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            if (!fi.Directory.Exists)
            {
                Directory.CreateDirectory(fi.DirectoryName);
                return false;
            }

            return true;
        }
    }
}

