using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2Groep7.Controllers;
using Moq;
using Projecten2Groep7.Models.Domain;
using Projecten2Groep7.Models;
using System.Web.Mvc;
using Projecten2Groep7.ViewModels;

namespace Projecten2Groep7.Tests.Controllers
{
    [TestClass]
    public class VerlanglijstControllerTest
    {
        private VerlanglijstController verlanglijstController;
        private Verlanglijst verlanglijst;
        private Mock<IProductRepository> productRepository;
        private Mock<IGebruikerRepository> gebruikerRepository;
        private Mock<IEmailRepository> emailRepository;
        private Mock<HttpContextBase> ctxMock;
        private Mock<ControllerContext> controllerCtxMock;
        private readonly DummyCatalogusContext dummyContext = new DummyCatalogusContext();

        [TestInitialize]
        public void TestInitializer()
        {
            productRepository = new Mock<IProductRepository>();
            gebruikerRepository = new Mock<IGebruikerRepository>();
            emailRepository = new Mock<IEmailRepository>();
          
            productRepository.Setup(p => p.FindAll()).Returns(dummyContext.Producten);
            productRepository.Setup(p => p.FindById(1)).Returns(dummyContext.Schatkist);
            productRepository.Setup(p => p.FindById(2)).Returns(dummyContext.Draaischijf);
            productRepository.Setup(p => p.FindById(3)).Returns(dummyContext.Splitsbomen);
            emailRepository.Setup(e => e.FindByReservatieStatus(ReservatieStatus.Gereserveerd)).Returns(dummyContext.Email);
            emailRepository.Setup(e => e.FindByReservatieStatus(ReservatieStatus.Geblokkeerd)).Returns(dummyContext.Email);

            ctxMock = new Mock<HttpContextBase>();
            controllerCtxMock = new Mock<ControllerContext>();
            controllerCtxMock.SetupGet(con => con.HttpContext).Returns(ctxMock.Object);

            verlanglijstController = new VerlanglijstController(gebruikerRepository.Object, productRepository.Object, emailRepository.Object)
            {
                ControllerContext = controllerCtxMock.Object
            };
            verlanglijst = new Verlanglijst();
        
            verlanglijst.VoegProductToe(dummyContext.Schatkist);
            
        }
        [TestMethod]
        public void IndexToontLegeVerlanglijstAlsVerlanglijstLeegIs()
        {
            ViewResult result = verlanglijstController.Index(dummyContext.CurrentGebruiker) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("LegeVerlanglijst", result.ViewName);
        }
        [TestMethod]
        public void IndexToontVerlanglijstAlsVerlanglijstNietLeegIs()
        {
            ViewResult result = verlanglijstController.Index(dummyContext.CurrentGebruiker) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("LegeVerlanglijst", result.ViewName);
        }
        [TestMethod]
        public void IndexZalVerlanglijstTonenAlsVerlanglijstNietLeegIs()
        {
            ViewResult result = verlanglijstController.Index(dummyContext.CurrentGebruiker) as ViewResult;
            Verlanglijst verlanglijstResult = result.ViewData.Model as Verlanglijst;
            Assert.AreEqual(1, verlanglijst.AantalItems);
        }
        [TestMethod]
        public void IndexZalViewMetVerlanglijstViewModelReturnen()
        {
            dummyContext.CurrentGebruiker.Verlanglijst.Producten = new List<Product>();
            dummyContext.CurrentGebruiker.Verlanglijst.Producten.Add(dummyContext.Schatkist);
            ViewResult result = verlanglijstController.Index(dummyContext.CurrentGebruiker) as ViewResult;
            Assert.IsInstanceOfType(result.Model, typeof(VerlanglijstViewModel));
        }
        [TestMethod]
        public void AddZalRedirectenNaarProduct()
        {
            RedirectToRouteResult result = verlanglijstController.Add(2, dummyContext.CurrentGebruiker) as RedirectToRouteResult;
            Assert.IsNotNull(result, "Should have redirected");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Product", result.RouteValues["controller"]);
            productRepository.Verify(p => p.FindById(2), Times.Once());
        }
        [TestMethod]
        public void AddZalProductToevoegenAanVerlanglijst() {
            RedirectToRouteResult result = verlanglijstController.Add(3, dummyContext.CurrentGebruiker) as RedirectToRouteResult;
            Assert.AreEqual(1, dummyContext.CurrentGebruiker.Verlanglijst.AantalItems);
            productRepository.Verify(p => p.FindById(3), Times.Once());
        }
        [TestMethod]
        public void RemoveZalRedirectenNaarIndex() {
            RedirectToRouteResult result = verlanglijstController.Remove(1, dummyContext.CurrentGebruiker) as RedirectToRouteResult;
            Assert.IsNotNull(result, "Should have redirected");
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
        [TestMethod]
        public void RemoveZalProductVerwijderenVanVerlanglijst() {
            RedirectToRouteResult result = verlanglijstController.Remove(1, dummyContext.CurrentGebruiker) as RedirectToRouteResult;
            Assert.AreEqual(0, dummyContext.CurrentGebruiker.Verlanglijst.AantalItems);
            productRepository.Verify(p => p.FindById(1), Times.Once());
        }
        [TestMethod]
        public void PlusDatumZalRedirectenNaarIndex()
        {
            VerlanglijstViewModel model = new VerlanglijstViewModel();
            DateTime van = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            DateTime tot = van.AddDays(7);
            model.StartDatum = van;
            model.EindDate = tot;
            RedirectToRouteResult result = verlanglijstController.PlusDatum(model, van, tot) as RedirectToRouteResult;
            Assert.IsNotNull(result, "Should have redirected");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Verlanglijst", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void MinDatumZalRedirectenNaarIndex()
        {
            VerlanglijstViewModel model = new VerlanglijstViewModel();
            DateTime van = DateTime.Now.AddDays(14).StartOfWeek(DayOfWeek.Monday);
            DateTime tot = van.AddDays(-7);
            model.StartDatum = van;
            model.EindDate = tot;
            RedirectToRouteResult result = verlanglijstController.MinDatum(model, van, tot) as RedirectToRouteResult;
            Assert.IsNotNull(result, "Should have redirected");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Verlanglijst", result.RouteValues["controller"]);
        }
        [TestMethod]
        public void MinDatumZalModelNietAanpassenIndienErNietTerugKanGegaanWorden()
        {
            VerlanglijstViewModel model = new VerlanglijstViewModel();
            DateTime eersteStartDatum = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            model.StartDatum = eersteStartDatum;
            model.EindDate = eersteStartDatum.AddDays(7);

            DateTime van = eersteStartDatum;
            DateTime tot = van.AddDays(-7);
            RedirectToRouteResult result = verlanglijstController.MinDatum(model, van, tot) as RedirectToRouteResult;
            Assert.AreEqual(eersteStartDatum, model.StartDatum);
            Assert.AreEqual(eersteStartDatum.AddDays(7), model.EindDate);
        }
        [TestMethod]
        public void GaNietTerugZalDisabledReturnenWanneerDatumOverschredenWordt()
        {
            DateTime terug = DateTime.Now.AddDays(-7);
            DateTime dateNu = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            Assert.AreEqual("disabled", verlanglijstController.GaNietTerug(terug, dateNu));
        }
        [TestMethod]
        public void GaNietTerugZalLegeStringReturnenWanneerDatumNietOverschredenWordt()
        {
            DateTime terug = DateTime.Now.AddDays(21);
            DateTime dateNu = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            Assert.AreEqual("", verlanglijstController.GaNietTerug(terug, dateNu));
        }

        [TestMethod]
        public void DetailsZalViewMetVerlanglijstDetailViewModelReturnen()
        {
            Dictionary<Product, int> p = new Dictionary<Product, int>();
            ICollection<Product> list = new List<Product>();
            list.Add(dummyContext.Schatkist);
            p.Add(dummyContext.Schatkist, 1);
            dummyContext.CurrentGebruiker.Verlanglijst.Producten.Add(dummyContext.Schatkist);
            int[] aantal = {1};
            DateTime van = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            DateTime tot = van.AddDays(7);
            Email[] email = { dummyContext.Email};
            dummyContext.CurrentGebruiker.VoegReservatieToe(p, van, tot, email);
            dummyContext.CurrentGebruiker.VoegReservatieToe(p, van.AddDays(14), tot.AddDays(14), email);

            VerlanglijstViewModel model = new VerlanglijstViewModel()
            {
                StartDatum = van,
                Producten = list,
                Aantallen = aantal,
                EindDate = tot
            };
            verlanglijstController.Reserveer(model, van, tot, dummyContext.CurrentGebruiker);
            ViewResult result = verlanglijstController.Details(2,dummyContext.CurrentGebruiker, van, tot) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(VerlanglijstDetailViewModel));
        }

        [TestMethod]
        public void ReserveerZalIndexViewEnVerlangLijstViewModelReturnenBijEenOngeldigAantal()
        {
            ICollection<Product> p = new List<Product>();
            p.Add(dummyContext.Schatkist);
            dummyContext.CurrentGebruiker.Verlanglijst.Producten.Add(dummyContext.Schatkist);
            int[] aantal = { -1 };
            DateTime van = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            DateTime tot = van.AddDays(7);
            dummyContext.CurrentGebruiker.Reservaties.Add(new Reservatie());
            VerlanglijstViewModel model = new VerlanglijstViewModel()
            {
                StartDatum = van,
                Producten = p,
                Aantallen = aantal,
                EindDate = tot
            };
            ViewResult result = verlanglijstController.Reserveer(model, van, tot, dummyContext.CurrentGebruiker) as ViewResult;
            Assert.IsInstanceOfType(result.Model, typeof(VerlanglijstViewModel));
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void ReserveerZalRedirectenNaarIndexMethodeenBijEenGeldigAantal()
        {
            ICollection<Product> p = new List<Product>();
            p.Add(dummyContext.Schatkist);
            dummyContext.CurrentGebruiker.Verlanglijst.Producten.Add(dummyContext.Schatkist);
            int[] aantal = { 1 };
            DateTime van = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            DateTime tot = van.AddDays(7);
            dummyContext.CurrentGebruiker.Reservaties.Add(new Reservatie());
            VerlanglijstViewModel model = new VerlanglijstViewModel()
            {
                StartDatum = van,
                Producten = p,
                Aantallen = aantal,
                EindDate = tot
            };
            RedirectToRouteResult result = verlanglijstController.Reserveer(model, van, tot, dummyContext.CurrentGebruiker) as RedirectToRouteResult;
            Assert.IsNotNull(result, "Should have redirected");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Product", result.RouteValues["controller"]);
        }
    }
}
