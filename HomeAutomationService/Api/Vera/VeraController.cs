using System.Collections.Generic;

namespace HomeAutomationService.Vera
{
    
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
        public string status { get; set; }
        public string level { get; set; }
        public string commFailure { get; set; }
        public int state { get; set; }
        public string comment { get; set; }
        public string ip { get; set; }
        public string url { get; set; }
        public string commands { get; set; }
        public string armed { get; set; }
        public string armedtripped { get; set; }
        public string pincodes { get; set; }
        public string locked { get; set; }
        public string batterylevel { get; set; }
        public string hvacstate { get; set; }
        public string message { get; set; }
        public string setpoint { get; set; }
        public string heat { get; set; }
        public string cool { get; set; }
        public string mode { get; set; }
        public string fanmode { get; set; }
        public string lasttrip { get; set; }
        public string tripped { get; set; }
        public string localapi { get; set; }
        public string servername { get; set; }
        public string version { get; set; }
        public string children { get; set; }
        public string lastdst { get; set; }
        public string running { get; set; }
        public string enabled { get; set; }
        public string invert { get; set; }
        public string retrig { get; set; }
        public string runtime { get; set; }
        public string tripcount { get; set; }
        public string since { get; set; }
        public string temperature { get; set; }
        public string text1 { get; set; }
        public string text2 { get; set; }
        public string status3 { get; set; }
        public string status4 { get; set; }
        public string status5 { get; set; }
        public string status6 { get; set; }
        public string status7 { get; set; }
        public string status8 { get; set; }
        public string radiobtns { get; set; }
        public string pulsebtns { get; set; }
        public string options { get; set; }
        public string msg { get; set; }
        public string status1 { get; set; }
        public string status2 { get; set; }
    }

    public class Category
    {
        public string name { get; set; }
        public int id { get; set; }
    }

    public class VeraController
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
        public string  mode { get; set; }
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