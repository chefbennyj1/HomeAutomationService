using Microsoft.Owin.Cors;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.StaticFiles.ContentTypes;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace HomeAutomationService
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Startup
    {        
        public void Configuration(IAppBuilder appBuilder)
        {


            appBuilder.UseCors(CorsOptions.AllowAll);
                                  
            
            appBuilder.UseFileServer(GetFileServerOptions());
            appBuilder.UseWebApi(GetWebApiHttpConfigurationOptions());
            

            ((HttpListener)appBuilder.Properties["System.Net.HttpListener"]).AuthenticationSchemes = AuthenticationSchemes.Basic | AuthenticationSchemes.Anonymous;



        }

        private static FileServerOptions GetFileServerOptions()
        {
            return new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = new PhysicalFileSystem("./Alexa/Configuration"),
                StaticFileOptions = { ContentTypeProvider = new CustomContentTypeProvider() },
                EnableDirectoryBrowsing = true
            };
        }

        private static HttpConfiguration GetWebApiHttpConfigurationOptions()
        {
            using (var config = new HttpConfiguration())
            {

                config.SuppressDefaultHostAuthentication();
                config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
                // Web API routes
                config.MapHttpAttributeRoutes(); 


                config.Routes.MapHttpRoute("FireTVService", "FireTV/{controller}/{id}",
                    new { id = RouteParameter.Optional });
                config.Routes.MapHttpRoute("HomeAutomationService", "GasPricesLocations/{controller}/{id}",
                    new { id = RouteParameter.Optional });

                config.MessageHandlers.Add(new CustomHeaderHandler());
                config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

                ForceJsonSerialization(config);

                return config;
            }
        }

        private static void ForceJsonSerialization(HttpConfiguration config)
        {
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter
            {
                SerializerSettings = { ContractResolver = new CamelCasePropertyNamesContractResolver() }
            });
        }
    }

    public class CustomContentTypeProvider : FileExtensionContentTypeProvider
    {
        public CustomContentTypeProvider()
        {
            Mappings.Add(".json", "application/json");
        }
    }

    public class CustomHeaderHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            response.Headers.Add("CACHE-CONTROL", "NO-CACHE");
            return response;
        }
    }
}

