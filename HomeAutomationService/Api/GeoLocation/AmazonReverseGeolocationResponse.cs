using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationService.Api.GeoLocation
{
    public class Region
    {
    }

    public class Nearest
    {
        public string inlatt { get; set; }
        public string distance { get; set; }
        public string timezone { get; set; }
        public string elevation { get; set; }
        public Region region { get; set; }
        public string name { get; set; }
        public string state { get; set; }
        public string latt { get; set; }
        public string longt { get; set; }
        public string city { get; set; }
        public string prov { get; set; }
        public string inlongt { get; set; }
        public string altgeocode { get; set; }
    }

    public class Osmtags
    {
        public string population { get; set; }
        public string wikidata { get; set; }
        public string wikipedia { get; set; }
        public string website { get; set; }

        public string name { get; set; }
        public string place { get; set; }

        public string state { get; set; }
        public string boundary { get; set; }
        public string id { get; set; }
        public string type { get; set; }
    }



    public class Major
{
    public string inlatt { get; set; }
    public string distance { get; set; }
    public string timezone { get; set; }
    public string elevation { get; set; }
    public string name { get; set; }
    public string state { get; set; }
    public string latt { get; set; }
    public string longt { get; set; }
    public string city { get; set; }
    public string prov { get; set; }
    public string inlongt { get; set; }

        public static implicit operator Major(string v)
        {
            throw new NotImplementedException();
        }
    }

    public class AmazonReverseGeolocationResponse
    {
        public string threegeonames { get; set; }
        public string geonumber { get; set; }
        public Nearest nearest { get; set; }
        public Osmtags osmtags { get; set; }
        public string geocode { get; set; }
        public Major major { get; set; }
    }
}


