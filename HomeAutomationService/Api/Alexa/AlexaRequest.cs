using System;
using System.Collections.Generic;

namespace HomeAutomationService.Alexa 
{
    public class Application
    {
        public string applicationId { get; set; }
    }

    public class AlexaDevicesAllGeolocationRead
    {
        public string status { get; set; }
    }

    public class Scopes
    {
        public AlexaDevicesAllGeolocationRead alexaDevicesAllGeolocationRead { get; set; }
    }

    public class Permissions
    {
        public string consentToken { get; set; }
        public Scopes scopes { get; set; }
    }

    public class User
    {
        public string userId { get; set; }
        public Permissions permissions { get; set; }
    }

    public class Session
    {
        public bool @new { get; set; }
        public string sessionId { get; set; }
        public Application application { get; set; }
        public User user { get; set; }
    }

    public class Application2
    {
        public string applicationId { get; set; }
    }

    public class AlexaDevicesAllGeolocationRead2
    {
        public string status { get; set; }
    }

    public class Scopes2
    {
        public AlexaDevicesAllGeolocationRead2 alexaDevicesAllGeolocationRead2 { get; set; }
    }

    public class Permissions2
    {
        public string consentToken { get; set; }
        public Scopes2 scopes { get; set; }
    }

    public class User2
    {
        public string userId { get; set; }
        public Permissions2 permissions { get; set; }
    }

    

    public class SupportedInterfaces
    {
        public Geolocation Geolocation { get; set; }
    }

    public class Device
    {
        public string deviceId { get; set; }
        public SupportedInterfaces supportedInterfaces { get; set; }
    }

    public class System
    {
        public Application2 application { get; set; }
        public User2 user { get; set; }
        public Device device { get; set; }
        public string apiEndpoint { get; set; }
        public string apiAccessToken { get; set; }
    }

    public class Coordinate
    {
        public double latitudeInDegrees { get; set; }
        public double longitudeInDegrees { get; set; }
        public double accuracyInMeters { get; set; }
    }

    public class Altitude
    {
        public double altitudeInMeters { get; set; }
        public double accuracyInMeters { get; set; }
    }

    public class Heading
    {
        public double directionInDegrees { get; set; }
    }

    public class Speed
    {
        public double speedInMetersPerSecond { get; set; }
    }

    public class Geolocation
    {
        public static object GetGeoLocation { get; internal set; }
        public DateTime timestamp { get; set; }
        public Coordinate coordinate { get; set; }
        public Altitude altitude { get; set; }
        public Heading heading { get; set; }
        public Speed speed { get; set; }
    }

    public class Context
    {
        public System System { get; set; }
        public Geolocation Geolocation { get; set; }
    }

    public class Intent
    {
        public string name { get; set; }
        public string confirmationStatus { get; set; }
    }

    public class Request
    {
        public string type { get; set; }
        public string requestId { get; set; }
        public DateTime timestamp { get; set; }
        public string locale { get; set; }
        public Intent intent { get; set; }
    }

    public class AlexaRequest
    {
        public string version { get; set; }
        public Session session { get; set; }
        public Context context { get; set; }
        public Request request { get; set; }
    }
}