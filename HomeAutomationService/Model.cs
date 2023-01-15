using System.Collections.Generic;

namespace HomeAutomationService
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Model
    {
        public static readonly Dictionary<string, bool> DeviceStates = new Dictionary<string, bool>
        {
                {"Fireplace", false},
                {"Computer Display", true }
        }; 
    }
}