using System.Collections.Generic;

namespace HomeAI2.HomeAutomation.Api.ZwaveDevices
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // ReSharper disable TooManyArguments
    // ReSharper disable MaximumChainedReferences

    public class Section
    {
        public string name { get; set; }
        public int id { get; set; }
    }

    public class Room
    {
        public string name { get; set; }
        public int id { get; set; }
        public int section { get; set; }
    }

    public class Scene
    {
        public string name { get; set; }
        public int id { get; set; }
        public int room { get; set; }
        public int state { get; set; }
        public string comment { get; set; }
        public int active { get; set; }
    }


    public class Device
    {
        public string name { get; set; }
        public string altid { get; set; }
        public int id { get; set; }
        public int category { get; set; }
        public int subcategory { get; set; }
        public int room { get; set; }
        public int parent { get; set; }
        public string ip { get; set; }
        public string commFailure { get; set; }
        public string level { get; set; }
        public string status { get; set; }
        public int? state { get; set; }
        public string comment { get; set; }
        public string fanmode { get; set; }
        public string mode { get; set; }
        public string setpoint { get; set; }
        public string heat { get; set; }
        public string cool { get; set; }
        public string hvacstate { get; set; }
        public string temperature { get; set; }
        public string armed { get; set; }
        public string armedtripped { get; set; }
        public string pincodes { get; set; }
        public string batterylevel { get; set; }
        public string locked { get; set; }
        public string lasttrip { get; set; }
        public string tripped { get; set; }
    }

    public class Category
    {
        public string name { get; set; }
        public int id { get; set; }
    }

    public class ZwaveHomeAutomationDeviceDto
    {
        public int full { get; set; }
        public string version { get; set; }
        public string model { get; set; }
        public int zwave_heal { get; set; }
        public string temperature { get; set; }
        public string skin { get; set; }
        public string serial_number { get; set; }
        public string fwd1 { get; set; }
        public string fwd2 { get; set; }
        public int mode { get; set; }
        public List<Section> sections { get; set; }
        public List<Room> rooms { get; set; }
        public List<Scene> scenes { get; set; }
        public List<Device> devices { get; set; }
        public List<Category> categories { get; set; }
        public int ir { get; set; }
        public string irtx { get; set; }
        public int loadtime { get; set; }
        public int dataversion { get; set; }
        public int state { get; set; }
        public string comment { get; set; }
    }
}