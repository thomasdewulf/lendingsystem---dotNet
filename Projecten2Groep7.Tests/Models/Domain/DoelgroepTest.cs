using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Tests.Models.Domain
{
    [TestClass]
    public class DoelgroepTest
    {
        private Doelgroep doelgroep;
        private Doelgroep doelgroepLeeg;

        [TestInitialize]
        public void Initialize()
        {
            doelgroep = new Doelgroep("Doelgroep");
            doelgroepLeeg = new Doelgroep()
            {
                DoelgroepId = 1
            };
        }
        [TestMethod]
        public void DoelgroepCorrectAangemaakt()
        {
            Assert.AreEqual("Doelgroep", doelgroep.Naam);
            Assert.IsNotNull(doelgroepLeeg);
            Assert.IsNotNull(doelgroep);
            Assert.AreEqual(1, doelgroepLeeg.DoelgroepId);
        }

    }
}
