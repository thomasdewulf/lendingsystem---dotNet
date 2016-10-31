using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2Groep7.Controllers;
using Moq;
using Projecten2Groep7.Models.Domain;
using System.Web.Mvc;
using System.Security.Principal;
using System.Web;
using System.Collections.Generic;

namespace Projecten2Groep7.Tests.Controllers
{
    [TestClass]
    public class ReservatieControllerTest
    {
        private ReservatieController reservatieController;
        private DummyCatalogusContext dummyContext;
        private Dictionary<Product, int> map;
        private DateTime van;
        private DateTime tot;

        [TestInitialize]
        public void TestInitializer()
        {
            reservatieController = new ReservatieController();
            dummyContext = new DummyCatalogusContext();
            map = new Dictionary<Product, int>();
            map.Add(dummyContext.Draaischijf, 1);
            map.Add(dummyContext.Schatkist, 1);

            van = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            tot = van.AddDays(7);
        }

        [TestMethod]
        public void IndexRetourneertLegeViewAlsErGeenReservatiesZijn()
        {
            ViewResult result = reservatieController.Index(dummyContext.CurrentGebruiker) as ViewResult;
            Assert.AreEqual("LegeReservaties", result.ViewName);
        }
        [TestMethod]
        public void IndexRetourneertViewMetLijnenAlsErReservatiesZijn()
        {
            Email[] email = { dummyContext.Email };
            dummyContext.CurrentGebruiker.VoegReservatieToe(map, van, tot, email);
            dummyContext.CurrentGebruiker.VoegReservatieToe(map, van.AddDays(14), tot.AddDays(14), email);
            ViewResult result = reservatieController.Index(dummyContext.CurrentGebruiker) as ViewResult;
            Assert.IsInstanceOfType(result.Model, typeof(ICollection<ReservatieLijn>));
        }
    }
}
