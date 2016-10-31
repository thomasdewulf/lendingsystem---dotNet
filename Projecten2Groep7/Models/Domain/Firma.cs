using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projecten2Groep7.Models.Domain
{
    public class Firma
    {
        public int FirmaId { get; set; }
        public string Naam { get; set; }
        public string Website { get; set; }
        [Display(Name = "Contactpersoon")]
        public string ContactPersoon { get; set; }
        [Display(Name="Email contactpersoon")]
        public string EmailContactPersoon { get; set; }
    }
}