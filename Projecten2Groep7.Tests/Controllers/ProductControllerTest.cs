using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2Groep7.Controllers;
using Projecten2Groep7.Models.Domain;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using Projecten2Groep7.ViewModels;

namespace Projecten2Groep7.Tests.Controllers
{
    [TestClass]
    public class ProductControllerTest
    {
        private ProductController productController;
        private Mock<IProductRepository> productRepository;
        private Mock<IGebruikerRepository> gebruikerRepository;
        private Mock<IDoelgroepRepository> doelgroepRepository;
        private Mock<ILeergebiedRepository> leergebiedRepository;
        private Mock<HttpContextBase> ctxMock;
        private Mock<ControllerContext> controllerCtxMock;
        private Mock<HttpRequestBase> requestBaseMock;
        private readonly DummyCatalogusContext dummyContext = new DummyCatalogusContext();



        [TestInitialize]
        public void TestInitializer()
        {
            productRepository = new Mock<IProductRepository>();
            gebruikerRepository = new Mock<IGebruikerRepository>();
            doelgroepRepository = new Mock<IDoelgroepRepository>();
            leergebiedRepository = new Mock<ILeergebiedRepository>();
            productRepository.Setup(p => p.FindAll()).Returns(dummyContext.Producten);
            doelgroepRepository.Setup(p => p.FindAll()).Returns(dummyContext.Doelgroepen);
            doelgroepRepository.Setup(p => p.FindById(1)).Returns(dummyContext.LagerOnderwijs);
            productRepository.Setup(p => p.FindById(1)).Returns(dummyContext.Schatkist);
            productRepository.Setup(p => p.FindById(2)).Returns(dummyContext.Draaischijf);
            productRepository.Setup(p => p.FindById(3)).Returns(dummyContext.Splitsbomen);
            productRepository.Setup(p => p.Add(It.IsNotNull<Product>()));
            productRepository.Setup(p => p.Delete(dummyContext.Schatkist));
            productRepository.Setup(p => p.SaveChanges());
            
            productRepository.Setup(p => p.Delete(dummyContext.Draaischijf)).Throws<Exception>();
            gebruikerRepository.Setup(g => g.FindAll()).Returns(dummyContext.Gebruikers);
            gebruikerRepository.Setup(g => g.SaveChanges());
            
            ctxMock = new Mock<HttpContextBase>();
            controllerCtxMock = new Mock<ControllerContext>();
            controllerCtxMock.SetupGet(con => con.HttpContext).Returns(ctxMock.Object);
            requestBaseMock = new Mock<HttpRequestBase>();
            ctxMock.SetupGet(r=>r.Request).Returns(requestBaseMock.Object);

            productController = new ProductController(productRepository.Object,doelgroepRepository.Object,leergebiedRepository.Object);
            productController.ControllerContext = controllerCtxMock.Object;
        }

        [TestMethod]
        public void IndexRetourneertAlleProductenGeordendAlsStudent()
        {
            ViewResult result = productController.Index(dummyContext.CurrentGebruiker,null,null,null,null) as ViewResult;
            List<ProductViewModel> models = (result.Model as IEnumerable<ProductViewModel>).ToList();
            Assert.AreEqual(2, models.Count);
            Assert.AreEqual("Draaischijf", models[0].Artikelnaam);
            Assert.AreEqual("Schatkist", models[1].Artikelnaam);
        }

        [TestMethod]
        public void IndexRetourneertSchatkistBijZoekenNaarSchatkistAlsStudent()
        {
            ViewResult result = productController.Index(dummyContext.CurrentGebruiker,"Schatkist",null,"Schatkist",null) as ViewResult;
            List<ProductViewModel> models = (result.Model as IEnumerable<ProductViewModel>).ToList();
            Assert.AreEqual(1, models.Count);
            Assert.AreEqual("Schatkist", models[0].Artikelnaam);
        }

        [TestMethod]
        public void IndexRetourneertAlleProductenGeordendAlsLector()
        {
            ViewResult result = productController.Index(dummyContext.CurrentGebruikerPersoneel,null,null,null,null) as ViewResult;
            List<ProductViewModel> models = (result.Model as IEnumerable<ProductViewModel>).ToList();
            Assert.AreEqual(3, models.Count);
            Assert.AreEqual("Draaischijf", models[0].Artikelnaam);
            Assert.AreEqual("Schatkist", models[1].Artikelnaam);
            Assert.AreEqual("Splitsbomen", models[2].Artikelnaam);
        }

        [TestMethod]
        public void IndexRetourneertSplitsbomenBijZoekenNaarSplitsbomenAlsLector()
        {
            ViewResult result = productController.Index(dummyContext.CurrentGebruikerPersoneel,"Splitsbomen",null,"Splitsbomen",null) as ViewResult;
            List<ProductViewModel> models = (result.Model as IEnumerable<ProductViewModel>).ToList();
            Assert.AreEqual(1, models.Count);
            Assert.AreEqual("Splitsbomen", models[0].Artikelnaam);
        }

        [TestMethod]
        public void IndexRetourneertGeenZoekresultatenViewBijZoekenNaarOnbestaandProductAlsLector()
        {
            ViewResult result = productController.Index(dummyContext.CurrentGebruikerPersoneel,"Product",null,"Product",null) as ViewResult;
            Assert.AreEqual("GeenZoekResultaten", result.ViewName);
        }
        [TestMethod]
        public void IndexRetourneertGeenZoekresultatenViewBijZoekenNaarOnbestaandProductAlsStudent()
        {
            ViewResult result = productController.Index(dummyContext.CurrentGebruiker, "Product", null, "Product",null) as ViewResult;
            Assert.AreEqual("GeenZoekResultaten", result.ViewName);
        }

        //[TestMethod]
        //public void IndexRetourneertAlleProductenVanDoelgroepLagerOnderwijsBijFilterenOpLagerOnderwijsAlsLector()
        //{
        //    SetUpUserMock("lector");
        //    ViewResult result = productController.Index(dummyContext.CurrentGebruiker, null, null, null, new string[]{"Lager onderwijs"}) as ViewResult;
        //    List<ProductViewModel> models = (result.Model as IEnumerable<ProductViewModel>).ToList();
        //    Assert.AreEqual(3, models.Count);
        //    Assert.AreEqual("Draaischijf", models[0].Artikelnaam);
        //    Assert.AreEqual("Schatkist", models[1].Artikelnaam);
        //    Assert.AreEqual("Splitsbomen", models[2].Artikelnaam);
        //}

        [TestMethod]
        public void DetailMethodeToontAlleDetailsVanProduct()
        {
            ViewResult result = productController.Details(1) as ViewResult;
            Product details = result.Model as Product;
            Product product = dummyContext.GetProduct(1);
            checkDetailScherm(product, details);
        }
        [TestMethod]
        public void DetailRetourneertHttpNotFoundAlsHetProductNietBestaat()
        {
            ActionResult result = productController.Details(4);
            Assert.IsInstanceOfType(result,typeof(HttpNotFoundResult));
        }
        [TestMethod]
        public void IsAlAanwezigInVerlanglijstZalDisabledStringRetournerenAlsHetInDeVerlanglijstAanwezigIs()
        {
            dummyContext.CurrentGebruiker.Verlanglijst.VoegProductToe(dummyContext.Draaischijf);
            string resultaat = productController.IsAlAanwezigInVerlanglijst(dummyContext.Draaischijf.ProductId, dummyContext.CurrentGebruiker);
            Assert.AreEqual("disabled", resultaat);
        }
        [TestMethod]
        public void IsAlAanwezigInVerlanglijstZalLegeStringRetournerenAlsHetNietInDeVerlanglijstAanwezigIs()
        {
            dummyContext.CurrentGebruiker.Verlanglijst.VoegProductToe(dummyContext.Draaischijf);
            string resultaat = productController.IsAlAanwezigInVerlanglijst(dummyContext.Schatkist.ProductId, dummyContext.CurrentGebruiker);
            Assert.AreEqual("", resultaat);
        }

        private void checkDetailScherm(Product product,Product details)
        {
            Assert.AreEqual(product.Artikelnaam, details.Artikelnaam);
            Assert.AreEqual(product.Omschrijving, details.Omschrijving);
            Assert.AreEqual(product.Artikelnummer, details.Artikelnummer);
            Assert.AreEqual(product.Prijs, details.Prijs);
            Assert.AreEqual(product.AantalInCatalogus, details.AantalInCatalogus);
            Assert.AreEqual(product.Uitleenbaar,details.Uitleenbaar);
            Assert.AreEqual(product.ProductId, details.ProductId);
            CollectionAssert.AreEqual(product.Doelgroepen.ToList(), details.Doelgroepen.ToList());
            CollectionAssert.AreEqual(product.Leergebieden.ToList(), details.Leergebieden.ToList());
        }

    }
}
