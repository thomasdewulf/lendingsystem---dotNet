using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projecten2Groep7.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Contact()
        {
            ViewBag.Message = "Gelieve ons te raadplegen via deze e-mailadressen.";

            return View();
        }
    }
}