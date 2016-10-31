using System.Web;
using Projecten2Groep7.Models.Domain;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Projecten2Groep7.Models;
using Projecten2Groep7.Models.DAL;

namespace Projecten2Groep7.Infrastructure
{
    public class GebruikerModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext.HttpContext.User.Identity.IsAuthenticated)
            {
                IGebruikerRepository repos = (GebruikerRepository)DependencyResolver.Current.GetService(typeof(GebruikerRepository));
                return repos.FindByUserName(HttpContext.Current.User.Identity.GetUserName());
            }
            return null;
        }
    }
}