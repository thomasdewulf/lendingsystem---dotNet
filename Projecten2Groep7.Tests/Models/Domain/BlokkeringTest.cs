using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Tests.Models.Domain
{
    [TestClass]
    public class BlokkeringTest
    {
        private Blokkering blokkering;

        [TestInitialize]
        public void Initialize()
        {
            blokkering = new Blokkering();
        }

        [TestMethod]
        public void InstantieVanBlokkeringWordtCorrectAangemaakt()
        {
            Assert.IsInstanceOfType(blokkering, typeof(Reservatie));
            Assert.IsNotNull(blokkering.ReservatieLijnen);
        }
    }
}
