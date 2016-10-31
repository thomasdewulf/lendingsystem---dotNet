using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2Groep7.Models.Domain;
using Projecten2Groep7.Tests.Controllers;
using System;

namespace Projecten2Groep7.Tests.Models.Domain
{
    [TestClass]
    public class ReservatieTest
    {
        private Reservatie reservatie;
        private DummyCatalogusContext context;

        [TestInitialize]
        public void Initialize()
        {
            context = new DummyCatalogusContext();
            reservatie = new Reservatie { ReservatieLijnen = new List<ReservatieLijn>() };
        }

        [TestMethod]
        public void ReservatieWordtCorrectAangemaakt()
        {
            Assert.AreEqual(0, reservatie.ReservatieLijnen.Count);
            Assert.IsNotNull(reservatie.ReservatieLijnen);
        }

        [TestMethod]
        public void VoegReservatieLijnToeVoegtReservatieLijnToeAanReservatie()
        {
            reservatie.VoegReservatieLijnToe(context.Schatkist, 1);
            Assert.AreEqual(1, reservatie.ReservatieLijnen.Count);
        }
    }
}
