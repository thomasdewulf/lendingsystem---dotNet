using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2Groep7.Controllers;

namespace Projecten2Groep7.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Contact()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Contact() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
