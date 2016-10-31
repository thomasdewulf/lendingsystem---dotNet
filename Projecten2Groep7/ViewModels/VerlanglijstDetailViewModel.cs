using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.ViewModels
{
    public class VerlanglijstDetailViewModel : ViewModel
    {
        public VerlanglijstDetailViewModel(Product product, IEnumerable<ReservatieLijn> reservatieLijnen )
        {
            this.Foto = product.Foto;
            this.Artikelnaam = product.Artikelnaam;
            this.Omschrijving = product.Omschrijving;
            this.Prijs = product.Prijs;
            this.ReservatieLijnen = reservatieLijnen;
            this.ProductDetail = product;
        }

        public VerlanglijstDetailViewModel()
        {
            throw new NotImplementedException();
        }

        [Display(Name = "Foto")]
        public string Foto
        {
            get;
            set;
        }

        [Display(Name = "Artikelnaam")]
        public string Artikelnaam
        {
            get;
            set;
        }

        [Display(Name = "Omschrijving")]
        public string Omschrijving
        {
            get;
            set;
        }

        [Display(Name = "Prijs")]
        public decimal Prijs
        {
            get;
            set;
        }

        public IEnumerable<ReservatieLijn> ReservatieLijnen { get; set; } 

        public Product ProductDetail { get; set; }
    }
}