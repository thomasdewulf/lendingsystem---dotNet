using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2Groep7.Models.Domain;
using Projecten2Groep7.Tests.Controllers;

namespace Projecten2Groep7.Tests.Models.Domain
{
    [TestClass]
    public class VerlanglijstTest
    {
        private Verlanglijst verlanglijst;
        private DummyCatalogusContext context;

        [TestInitialize]
        public void Initialize()
        {
            context = new DummyCatalogusContext();
            verlanglijst = new Verlanglijst();
        }
        [TestMethod]
        public void VerlanglijstWordtCorrectAangemaakt()
        {
            Assert.IsNotNull(verlanglijst.Producten);
        }
        [TestMethod]
        public void VoegProductToeVoegtProductToeAanVerlaanglijstIndienHetNogNietAanwezigIs()
        {
            verlanglijst.VoegProductToe(context.Schatkist);
            Assert.AreEqual(1, verlanglijst.Producten.Count);
            Assert.AreEqual(context.Schatkist, verlanglijst.GeefProduct(context.Schatkist.ProductId));
        }
        [TestMethod]
        public void VoegProductToeVoegtProductNietToeAanVerlaanglijstIndienHetAanwezigIs()
        {
            verlanglijst.VoegProductToe(context.Schatkist);
            verlanglijst.VoegProductToe(context.Schatkist);
            Assert.AreEqual(1, verlanglijst.Producten.Count);
            Assert.AreEqual(context.Schatkist, verlanglijst.GeefProduct(context.Schatkist.ProductId));
        }
        [TestMethod]
        public void MaakVerlanglijstLeegMoetAlleProductenUitDeVerlanglijstWissen()
        {
            verlanglijst.VoegProductToe(context.Draaischijf);
            verlanglijst.MaakVerlanglijstLeeg();
            Assert.AreEqual(0, verlanglijst.Producten.Count);
        }
        [TestMethod]
        public void GeefProductGeeftCorresponderendProductTerugUitVerlanglijst()
        {
            verlanglijst.VoegProductToe(context.Splitsbomen);
            Product p = verlanglijst.GeefProduct(context.Splitsbomen.ProductId);
            Assert.AreEqual(context.Splitsbomen, p);
        }
        [TestMethod]
        public void VerwijderProductVerwijdertHetMeegegevenProductUitDeVerlanglijstAlsHetProductInDeVerlanglijstAanwezigIs()
        {
            verlanglijst.VoegProductToe(context.Schatkist);
            verlanglijst.VoegProductToe(context.Splitsbomen);
            verlanglijst.VerwijderProduct(context.Schatkist);
            Assert.AreEqual(1,verlanglijst.Producten.Count);
        }
        [TestMethod]
        public void VerwijderProductVerwijdertHetMeegegevenProductUitDeVerlanglijstAlsHetProductNietInDeVerlanglijstAanwezigIs()
        {
            verlanglijst.VoegProductToe(context.Schatkist);
            verlanglijst.VoegProductToe(context.Splitsbomen);
            verlanglijst.VerwijderProduct(context.Draaischijf);
            Assert.AreEqual(2, verlanglijst.Producten.Count);
        }
        [TestMethod]
        public void BevatProductRetourneertTrueAlsHetProductAanwezigIsInDeVerlanglijst()
        {
            verlanglijst.VoegProductToe(context.Draaischijf);
            Assert.IsTrue(verlanglijst.BevatProduct(context.Draaischijf));
        }
        [TestMethod]
        public void BevatProductRetourneertFalseAlsHetProductNietAanwezigIsInDeVerlanglijst()
        {
            verlanglijst.VoegProductToe(context.Draaischijf);
            Assert.IsFalse(verlanglijst.BevatProduct(context.Schatkist));
        }
    }
}
