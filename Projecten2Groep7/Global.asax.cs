using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Projecten2Groep7.Infrastructure;
using Projecten2Groep7.Models;

namespace Projecten2Groep7
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //ModelBinders.Binders.Add(typeof(Verlanglijst), new VerlanglijstModelBinder());
            ModelBinders.Binders.Add(typeof(ApplicationUser), new GebruikerModelBinder());
        }
    }
}
    