using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Tests.Models.Domain
{
    [TestClass]
    public class LeergebiedTest
    {
        private Leergebied leergebied;
        private Leergebied leergebiedLeeg;

        [TestInitialize]
        public void Initialize()
        {
            leergebied = new Leergebied("Leergebied");
            leergebiedLeeg = new Leergebied()
            {
                LeergebiedId = 1
            };
        }

        [TestMethod]
        public void LeergebiedCorrectAangemaakt()
        {
            Assert.AreEqual("Leergebied", leergebied.Naam);
            Assert.IsNotNull(leergebied);
            Assert.IsNotNull(leergebiedLeeg);
            Assert.AreEqual(1, leergebiedLeeg.LeergebiedId);
        }
    }
}
