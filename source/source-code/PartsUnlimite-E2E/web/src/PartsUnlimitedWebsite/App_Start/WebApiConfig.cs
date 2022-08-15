using System.Web.Http;
using Microsoft.Practices.Unity;
using Unity.WebApi;

namespace PartsUnlimited
{
    public class WebApiConfig
    {
        public static void RegisterWebApi(HttpConfiguration config, IUnityContainer container)
        {
            config.DependencyResolver = new UnityDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            

        }
    }
}