using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeAutomationService.Alexa;
using HomeAutomationService.Helpers;

namespace HomeAutomationService.Api.GeoLocation
{
    public class GeoLocator
    {
        public static AmazonReverseGeolocationResponse GetLocation(AlexaRequest alexaRequest)
        {
           

                var longitudeInDegrees = alexaRequest.context.Geolocation.coordinate.longitudeInDegrees;
                var latitudeInDegrees = alexaRequest.context.Geolocation.coordinate.latitudeInDegrees;

                var url = "https://api.3geonames.org/" + latitudeInDegrees + "," + longitudeInDegrees + ".json";

                var json = HttpClient.GET(url);
                return new NewtonsoftJsonSerializer().DeserializeFromString<AmazonReverseGeolocationResponse>(json);
            
            
        }
    }
}
