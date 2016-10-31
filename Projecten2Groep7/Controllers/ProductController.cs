using Microsoft.AspNet.Identity;
using Projecten2Groep7.Models.DAL.Mapper;
using Projecten2Groep7.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;
using PagedList;
using Projecten2Groep7.Models;
using Projecten2Groep7.ViewModels;

namespace Projecten2Groep7.Controllers
{
    //
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IDoelgroepRepository doelgroepRepository;
        private readonly ILeergebiedRepository leergebiedRepository;
        private ViewModelFactory viewModelFactory;
        
        public ProductController(IProductRepository productRepository,IDoelgroepRepository doelgroepRepository,ILeergebiedRepository leergebiedRepository)
        {
            this.productRepository = productRepository;
            this.doelgroepRepository = doelgroepRepository;
            this.leergebiedRepository = leergebiedRepository;
            viewModelFactory = new ViewModelFactory();
        }
        // GET: Product
        public ActionResult Index(ApplicationUser gebruiker,string currentFilter, int? page, string searchString = null, string doelgroep = null,string leergebied = null)
        {
            ViewBag.Gebruiker = gebruiker;
            IEnumerable<Product> producten = productRepository.FindAll().ToList();
            
            if(gebruiker is Student)
                producten = producten.Where(b => b.Uitleenbaar).ToList();

            Verlanglijst verlanglijst = gebruiker.Verlanglijst;
            IEnumerable<ProductViewModel> productlijst = null;
            if (verlanglijst != null)
            {
                productlijst = producten.Select(p => new ProductViewModel(p, verlanglijst.BevatProduct(p), doelgroep)).ToList();
            }
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            
            ViewBag.CurrentFilter = searchString;
            ViewBag.Doelgroepen = doelgroepRepository.FindAll().ToArray();
            ViewBag.Leergebieden = leergebiedRepository.FindAll().ToArray();
      
            if (doelgroep != null)
            {
              
               productlijst = productlijst.Where(p => p.Doelgroepen.Contains(doelgroepRepository.FindByName(doelgroep)));
               
            }
            if (leergebied != null)
            {
                productlijst =
                    productlijst.Where(p => p.Leergebieden.Contains(leergebiedRepository.FindByName(leergebied)));
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim().ToLower();
                productlijst =
                    productlijst.Where(
                        n =>
                            n.Artikelnaam.ToLower().Contains(searchString) ||
                            n.Omschrijving.ToLower().Contains(searchString));
                //|| n.BevatDoelgroep(searchString) || n.BevatLeergebied(searchString));
                if (productlijst.Count() == 0)
                {
                    return View("GeenZoekResultaten");
                }
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            if (Request.IsAjaxRequest())
                return PartialView("Producten", productlijst.ToPagedList(pageNumber, pageSize));
            
            return View(productlijst.ToPagedList(pageNumber,pageSize));
        }
        
        public string IsAlAanwezigInVerlanglijst(int productNummer, ApplicationUser gebruiker)
        {
            Product product = productRepository.FindById(productNummer);

            if (gebruiker.Verlanglijst.Producten.Contains(product))
            {
                return "disabled";
            }
            return "";
        }
        public ActionResult Details(int id)
        {
            Product product = productRepository.FindById(id);
            if(product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


    }
}