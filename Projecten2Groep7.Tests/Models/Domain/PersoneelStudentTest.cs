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
    public class PersoneelStudentTest
    {
        private ApplicationUser student;
        private ApplicationUser personeel;
        private Dictionary<Product, int> map;
        private DateTime van;
        private DateTime tot;
        private int reservatieId;
        private DummyCatalogusContext context;
        private Reservatie reservatiePersoneel;
        private int aantalBeschikbaar;
        private bool[] dagen = new bool[5];

        [TestInitialize]
        public void Initialize()
        {
            context = new DummyCatalogusContext();
            student = context.CurrentGebruiker;
            personeel = context.CurrentGebruikerPersoneel;

            map = new Dictionary<Product, int>();
            van = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            tot = van.AddDays(7);
            reservatieId = 0;
            dagen[0] = true;
            dagen[1] = false;
            dagen[2] = false;
            dagen[3] = true;
            dagen[4] = true;
        }

        [TestMethod]
        public void VoegReservatieToeMaaktBlokkeringAanVoorPersoneel()
        {
            Email[] email = { context.Email };
            personeel.Verlanglijst.VoegProductToe(context.Draaischijf);
            map.Add(context.Draaischijf, 1);
            personeel.VoegReservatieToe(map, van, tot, email, dagen);
            aantalBeschikbaar =
                personeel.Verlanglijst.GeefProduct(context.Draaischijf.ProductId)
                    .GeefAantalReserveerbaarInPeriode(van, tot);
            Assert.AreEqual(0,aantalBeschikbaar);
            Assert.AreEqual(1,personeel.Reservaties.Count);
        }
        [TestMethod]
        public void VoegReservatieToePastOverrulingToeWanneerErGeenGenoegProductenBeschikbaarZijn()
        {
            Email[] email = { context.Email, context.Email, context.Email};
            student.Verlanglijst.VoegProductToe(context.Schatkist);
            map.Add(context.Schatkist, 1);
            student.VoegReservatieToe(map, van, tot, email);
            
            personeel.Verlanglijst.VoegProductToe(context.Schatkist);
            map.Clear();
            map.Add(context.Schatkist, 1);
            personeel.VoegReservatieToe(map, van, tot, email, dagen);

            aantalBeschikbaar =
                personeel.Verlanglijst.GeefProduct(context.Schatkist.ProductId)
                    .GeefAantalReserveerbaarInPeriode(van, tot);
            reservatiePersoneel = personeel.Reservaties.First(r => r.ReservatieId == reservatieId);
            int studentAantal = student.Reservaties.First(r=>r.ReservatieId == reservatieId).ReservatieLijnen
                .First(f=>f.Product.Equals(context.Schatkist)).Aantal;
            int personeelAantal = personeel.Reservaties.First(r => r.ReservatieId == reservatieId).ReservatieLijnen
                .First(f => f.Product.Equals(context.Schatkist)).Aantal;

            Assert.AreEqual(0, aantalBeschikbaar);
            Assert.AreEqual(1, student.Reservaties.Count);
            Assert.AreEqual(1, personeel.Reservaties.Count);
            Assert.AreEqual(ReservatieStatus.Geblokkeerd, reservatiePersoneel.ReservatieStatus);
            Assert.AreEqual(1, personeelAantal);
            Assert.AreEqual(0, studentAantal);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void VoegReserveringToeThrowedExceptionAlsAantalGroterIsAlsAantalInCatalogus()
        {
            Email[] email = { context.Email };
            student.Verlanglijst.VoegProductToe(context.Draaischijf);
            map.Add(context.Draaischijf, 3);
            student.VoegReservatieToe(map, van, tot, email);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void VoegReserveringToeThrowedExceptionAlsErEenNegatiefAantalWordtMeegegeven()
        {
            Email[] email = { context.Email };
            student.Verlanglijst.VoegProductToe(context.Draaischijf);
            map.Add(context.Draaischijf, -3);
            student.VoegReservatieToe(map, van, tot, email);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VoegReserveringToeThrowedExceptionAlsErGeenDagenAangeduidZijnVoorLector()
        {
            dagen[0] = false;
            dagen[1] = false;
            dagen[2] = false;
            dagen[3] = false;
            dagen[4] = false;
            Email[] email = { context.Email };
            personeel.Verlanglijst.VoegProductToe(context.Draaischijf);
            map.Add(context.Draaischijf, 1);
            personeel.VoegReservatieToe(map, van, tot, email, dagen);
        }
    }
}
