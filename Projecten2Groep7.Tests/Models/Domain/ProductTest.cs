using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2Groep7.Models.Domain;
using Projecten2Groep7.Tests.Controllers;

namespace Projecten2Groep7.Tests.Models.Domain
{
    [TestClass]
    public class ProductTest
    {
        private Product product;
        private DummyCatalogusContext ctx;

        [TestInitialize]
        public void Initialize()
        {
            ctx = new DummyCatalogusContext();
            product = ctx.Draaischijf;
        }

        [TestMethod]
        public void NieuwProductCorrectAangemaakt()
        {
            Assert.IsNotNull(product.Doelgroepen);
            Assert.IsNotNull(product.Leergebieden);
        }

        [TestMethod]
        public void BevatDoelgroepEnBevatLeergebiedGevenTrueTerugBijEenJuisteDoelgroepOfLeergebied()
        {
            Assert.AreEqual(true, product.BevatDoelgroep("Lager onderwijs"));
            Assert.AreEqual(true, product.BevatLeergebied("Kind"));
        }

        [TestMethod]
        public void BevatDoelgroepEnBevatLeergebiedGevenFalseTerugBijEenOnbestaandeDoelgroepOfLeergebied()
        {
            Assert.AreEqual(false, product.BevatDoelgroep("Volwassenen"));
            Assert.AreEqual(false, product.BevatLeergebied("HogerOnderwijs"));
        }
    }
}
