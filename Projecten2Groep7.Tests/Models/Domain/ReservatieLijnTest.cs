using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2Groep7.Models.Domain;
using Projecten2Groep7.Tests.Controllers;
using Projecten2Groep7.Models;

namespace Projecten2Groep7.Tests.Models.Domain
{
    [TestClass]
    public class ReservatieLijnTest
    {
        private ReservatieLijn reservatieLijn;
        private Reservatie reservatie;
        private ApplicationUser student;
        private Dictionary<Product, int> map;
        private DateTime van;
        private DateTime tot;
        private int reservatieId;
        private int reservatieLijnId;
        private DummyCatalogusContext context;

        [TestInitialize]
        public void Initialize()
        {
            context = new DummyCatalogusContext();
            student = context.CurrentGebruiker;

            map = new Dictionary<Product, int>();
            van = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            tot = van.AddDays(7);
            reservatieId = 0;

            Email[] mail = { context.Email};
            student.Verlanglijst.VoegProductToe(context.Draaischijf);
            map.Add(context.Draaischijf, 1);
            student.VoegReservatieToe(map, van, tot, mail);
            reservatie = student.Reservaties.First(r => r.ReservatieId == reservatieId);

            reservatieLijn =
                student.Reservaties.First(r => r.ReservatieId == reservatieId)
                    .ReservatieLijnen.First(l => l.Product.Equals(context.Draaischijf));            
        }
        [TestMethod]
        public void GeefStartDatumVoorReservatieGeeftCorrecteDatumTerug()
        {
            Assert.AreEqual(van,reservatieLijn.GeefStartDatumVoorReservatie());
        }
        [TestMethod]
        public void GeefEindDatumVoorReservatieGeeftCorrecteDatumTerug()
        {
            Assert.AreEqual(tot, reservatieLijn.GeefEindDatumVoorReservatie());
        }
        [TestMethod]
        public void GeefReservatieUserGeeftCorrecteUserTerug()
        {
            Assert.AreEqual(student, reservatieLijn.GeefReservatieUser());
        }
        [TestMethod]
        public void GeefAanmaakDatumVoorReservatieGeeftCorrecteDatumTerug()
        {
            Assert.AreEqual(reservatie.AanmaakDatum, reservatieLijn.GeefAanmaakDatumVoorReservatie());
        }
        [TestMethod]
        public void ReservatieLijnWordtCorrectAangemaakt()
        {
            reservatieLijnId = 0;
            Assert.AreEqual(reservatieLijnId, reservatieLijn.ReservatieLijnId);
        }

    }
}
