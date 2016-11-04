using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using Castle.Windsor;
using Food.WebApi.Plumbing;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace Food.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, IWindsorContainer container)
        {
            RegisterExceptionLogger(config);
            RegisterExceptionHandler(config);
            RegisterDelegateHandler(config);
            ConfigureAuthentication(config);
            MapRoutes(config);
            //RegisterWindsorResolver(config, container);
            RegisterControllerActivator(container);
        }

        private static void RegisterDelegateHandler(HttpConfiguration config)
        {
            GlobalConfiguration.Configuration.MessageHandlers.Add(new RequestAndReponseLogHandler());
        }

        private static void RegisterExceptionLogger(HttpConfiguration config)
        {
            config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
        }

        private static void RegisterExceptionHandler(HttpConfiguration config)
        {
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
        }

        private static void RegisterWindsorResolver(HttpConfiguration config, IWindsorContainer container)
        {
            var dependencyResolver = new WindsorDependencyResolver(container);
            config.DependencyResolver = dependencyResolver;
        }

        private static void ConfigureAuthentication(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
        }

        private static void MapRoutes(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            /*config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );*/
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
        }


        public static void RegisterControllerActivator(IWindsorContainer windoContainer)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator),
                new WindsorCompositionRoot(windoContainer));
        }
    }
}
