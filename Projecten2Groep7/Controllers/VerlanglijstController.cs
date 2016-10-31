using Projecten2Groep7.Models.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using Projecten2Groep7.Models;
using Projecten2Groep7.ViewModels;

namespace Projecten2Groep7.Controllers
{
    [Authorize]
    public class VerlanglijstController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IGebruikerRepository gebruikerRepository;
        private readonly IEmailRepository emailRepository;

        private ViewModelFactory viewModelFactory;
        public VerlanglijstController(IGebruikerRepository gebruikerRepository, IProductRepository productRepository, IEmailRepository emailRepository)
        {
            this.productRepository = productRepository;
            this.gebruikerRepository = gebruikerRepository;
            this.emailRepository = emailRepository;
            viewModelFactory = new ViewModelFactory();

        }
        // GET: Verlanglijst
        public ActionResult Index(ApplicationUser user, VerlanglijstViewModel m = null)
        {
            if (user.Verlanglijst.AantalItems == 0)
                return View("LegeVerlanglijst");
            //Dictionary<Product,int> map = new Dictionary<Product, int>();

            VerlanglijstViewModel model = (VerlanglijstViewModel)viewModelFactory.CreateViewModel("verlanglijst");
            if (m != null)
            {
                model = m;
            }
            //user.Verlanglijst.Producten.ForEach(p => model.ProductenMap.Add(p, 0));
            model.Producten = user.Verlanglijst.Producten;
            Dictionary<int, bool[]> d = new Dictionary<int, bool[]>();
            foreach (var item in model.Producten)
            {
                d.Add(item.ProductId, new bool[5]);
            }
            ViewBag.ProductenLector = d;
            return View(model);
        }

        public ActionResult Add(int id, ApplicationUser user)
        {
            Product product = productRepository.FindById(id);

            if (product != null)
            {
                if (user.Verlanglijst.BevatProduct(product))
                {
                    TempData["Error"] =  "De verlanglijst bevat reeds product " + product.Artikelnaam +".";
                }
                else
                {
                    user.Verlanglijst.VoegProductToe(product);
                    TempData["Info"] = "Product " + product.Artikelnaam + " is toegevoegd aan je verlanglijst";
                    gebruikerRepository.SaveChanges();
                }
            }
            return RedirectToAction("Index","Product");
        }


        public ActionResult Remove(int id, ApplicationUser user)
        {
            Product product = productRepository.FindById(id);
            user.Verlanglijst.VerwijderProduct(product);
            TempData["Info"] = "Product " + product.Artikelnaam + " is verwijderd van je verlanglijst";
            gebruikerRepository.SaveChanges();
            return RedirectToAction("Index","Verlanglijst");
        }

        [HttpPost]
        public ActionResult Reserveer(VerlanglijstViewModel viewModel, DateTime van, DateTime tot, ApplicationUser user, bool[] dagenLector = null)
        {
            try {
            Product[] producten = user.Verlanglijst.Producten.ToArray();
                if (ModelState.IsValid)
            {
                Dictionary<Product, int> productAantalMap = new Dictionary<Product, int>();
                for (int i = 0; i < viewModel.Aantallen.Length; i++)
                {
                    productAantalMap.Add(producten[i], viewModel.Aantallen[i]);
                }
                    if (user is Student)
                {
                    Email[] emails= new Email[1];
                    emails[0] = emailRepository.FindByReservatieStatus(ReservatieStatus.Gereserveerd);
                    user.VoegReservatieToe(productAantalMap, van, tot, emails);
                }
                if (user is Personeel)
                {
                    Email[] emails = new Email[3];
                    Email email = emailRepository.FindByReservatieStatus(ReservatieStatus.Geblokkeerd);
                    Email email2 = emailRepository.FindByReservatieStatus(ReservatieStatus.Geannuleerd);
                    Email email3 = emailRepository.FindByReservatieStatus(ReservatieStatus.Gewijzigd);
                    emails[0] = email; emails[1] = email2; emails[2] = email3;
                    user.VoegReservatieToe(productAantalMap, van,tot, emails,dagenLector);   
                }
                gebruikerRepository.SaveChanges();
                TempData["Info"] = "Uw reservatie is succesvol toegevoegd.";
                return RedirectToAction("Index", "Product");
            }
            }catch(ArgumentNullException ex ) {            
                ModelState.AddModelError("key", ex.Message);
            }catch (ArgumentOutOfRangeException ex)
            {
                ModelState.AddModelError("key", ex.Message);
            }
            viewModel.Producten = user.Verlanglijst.Producten;
            return View("Index", viewModel);
        }
        public ActionResult PlusDatum(VerlanglijstViewModel m, DateTime van, DateTime tot)
        {
            var vanDatum = van;
            var eindDatum = tot;
            vanDatum = vanDatum.AddDays(7.0);
            eindDatum = eindDatum.AddDays(7.0);
            VerlanglijstViewModel model = new VerlanglijstViewModel() { StartDatum = vanDatum, EindDate = eindDatum };
            return RedirectToAction("Index", "Verlanglijst", model);
        }

        public ActionResult MinDatum(VerlanglijstViewModel m,DateTime van, DateTime tot)
        {
            DateTime hulp = van.AddDays(-7);
            DateTime dateNu = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            VerlanglijstViewModel model = new VerlanglijstViewModel();
            if (hulp > dateNu && van >= hulp)
            {
                    van = van.AddDays(-7);
                    tot = tot.AddDays(-7);
                    model = new VerlanglijstViewModel() {StartDatum = van, EindDate = tot};
            }
            return RedirectToAction("Index", "Verlanglijst", model);
        }

        public string GaNietTerug(DateTime van, DateTime tot)
        {
            DateTime hulp = van.AddDays(-7);
            DateTime dateNu = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            if (hulp < dateNu || van < hulp)
            {
                return "disabled";
            }
            return "";
        }

        public ActionResult TekenGrafiek(ApplicationUser user, string naam)
        {
            Product p = user.Verlanglijst.Producten.First(pr=>pr.Artikelnaam == naam);
            int totaal = p.AantalInCatalogus;
            DateTime[] weken = new DateTime[12];
            int[] aantal = new int[12];
            DateTime nu = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            for (int j = 0; j < 12; j++)
            {
                weken[j] = nu;
                aantal[j] = p.GeefAantalReserveerbaarInPeriode(nu, nu.AddDays(7));
                nu = nu.AddDays(7);
            }
            Bitmap image = new Bitmap(300, 50);
            Graphics g = Graphics.FromImage(image);
            Chart c = new Chart();
            ChartArea a = new ChartArea();

            a.AxisX.MajorGrid.Enabled = false;
            a.BackColor = Color.White;
            c.Titles.Add("Aantal beschikbaar komende weken");
            c.ChartAreas.Add(a);
            c.Width = 1000;
            c.Height = 250;
            Series mySeries = new Series();
            mySeries.Points.DataBindXY(weken, aantal);
            mySeries.IsValueShownAsLabel = true;
            mySeries.Color = Color.FromArgb(0, 156, 124);
            a.AxisX.Interval = 7;
            c.Series.Add(mySeries);

            MemoryStream imageStream = new MemoryStream();
            c.SaveImage(imageStream, ChartImageFormat.Png);
            c.TextAntiAliasingQuality = TextAntiAliasingQuality.SystemDefault;
            Response.ContentType = "image/png";
            imageStream.WriteTo(Response.OutputStream);
            g.Dispose();
            image.Dispose();
            return null;
        }

        public ActionResult Details(int id, ApplicationUser user, DateTime van, DateTime tot)
        {

          ICollection<ReservatieLijn> lijnen = new List<ReservatieLijn>();
            Product product = productRepository.FindById(id);
            ICollection<Reservatie> reservaties = product.GeefAlleReservatiesMetStatusGeBlokkeerdEnGereserveerd();
      
            foreach (var reservatie in reservaties)
            {
                foreach (var reservatielijn in reservatie.ReservatieLijnen)
                {
                    if (reservatielijn.Product == product && DateTime.Compare(reservatielijn.GeefStartDatumVoorReservatie(),van) >= 0 &&
                        DateTime.Compare(reservatielijn.GeefEindDatumVoorReservatie(), tot)<= 0)
                    {
                        lijnen.Add(reservatielijn);
                    }
                }
               

            }

            return View(new VerlanglijstDetailViewModel(product,lijnen));
        }
    }
}