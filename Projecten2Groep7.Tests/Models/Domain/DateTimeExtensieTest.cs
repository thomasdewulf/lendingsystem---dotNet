using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Tests.Models.Domain
{
    [TestClass]
    public class DateTimeExtensieTest
    {
        private DateTime vanDinsdag;
        private DateTime vanDonderdag;
        private DateTime verwacht1;
        private DateTime verwacht2;
        private DateTime vanVrijdagVoor17Uur;
        private DateTime vanVrijdagNa17Uur;

        [TestInitialize]
        public void Initialize()
        {
            vanDinsdag = new DateTime(2016, 3, 1);
            vanDonderdag = new DateTime(2016, 3, 3);
            vanVrijdagVoor17Uur = new DateTime(2016, 3, 4);
            vanVrijdagVoor17Uur = vanVrijdagVoor17Uur.AddHours(16);
            vanVrijdagNa17Uur = new DateTime(2016, 3, 4);
            vanVrijdagNa17Uur = vanVrijdagNa17Uur.AddHours(17).AddMinutes(1);

            verwacht1 = new DateTime(2016, 3, 7);
            verwacht2 = new DateTime(2016, 3, 14);
        }
        [TestMethod]
        public void StartOfWeekGeeftDeVolgendeBeschikbareMaandagWanneerErOpDinsdagGereserveerdWordt()
        {
            Assert.AreEqual(verwacht1,DateTimeExtensie.StartOfWeek(vanDinsdag, DayOfWeek.Monday));
        }
        [TestMethod]
        public void StartOfWeekGeeftVolgendeBeschikbareMaandagWanneerErVrijdagVoor17UurGereserveerdWordt()
        {
            Assert.AreEqual(verwacht1, DateTimeExtensie.StartOfWeek(vanVrijdagVoor17Uur, DayOfWeek.Monday));
        }
        [TestMethod]
        public void StartOfWeekGeeftVolgendeBeschikbareMaandagWanneerErVrijdagNa17UurGereserveerdWordt()
        {
            Assert.AreEqual(verwacht2, DateTimeExtensie.StartOfWeek(vanVrijdagNa17Uur, DayOfWeek.Monday));
        }
        [TestMethod]
        public void StartOfWeekGeeftVolgendeBeschikbareMaandagWanneerErOpDonderdagGereserveerdWordt()
        {
            Assert.AreEqual(verwacht1, DateTimeExtensie.StartOfWeek(vanDonderdag, DayOfWeek.Monday));
        }
    }
}
