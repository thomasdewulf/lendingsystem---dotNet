using Projecten2Groep7.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projecten2Groep7.ViewModels
{
    public class ProductViewModel : ViewModel
    {
        public ProductViewModel()
        {
            
        }
        public ProductViewModel(Product product,bool zitInVerlanglijst,string checkbox)
        {
            this.ProductId = product.ProductId;
            this.Artikelnaam = product.Artikelnaam;
            this.Omschrijving = product.Omschrijving;
            this.Foto = product.Foto;
            this.AantalInCatalogus = product.AantalInCatalogus;
            this.zitInVerlanglijst = zitInVerlanglijst;
            this.Doelgroepen = product.Doelgroepen;
            this.Leergebieden = product.Leergebieden;


        }
        public int ProductId { get; set; }

        [Display(Name = "Artikelnaam")]
        public string Artikelnaam { get; set; }
        
        [Display(Name = "Omschrijving")]
        public string Omschrijving { get; set; }
       
        [Display(Name = "Foto")]
        public string Foto { get; set; }

        [Display(Name = "Aantal")]
        public int AantalInCatalogus { get; set; }

        [Display(Name = "Op verlanglijst")]
        public bool zitInVerlanglijst { get; set; }

        public string checkbox { get; set; }

        public ICollection<Doelgroep> Doelgroepen { get; set; }
        public ICollection<Leergebied> Leergebieden { get; set; }

    }
}