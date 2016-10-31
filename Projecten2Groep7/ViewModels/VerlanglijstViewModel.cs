using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projecten2Groep7.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Projecten2Groep7.ViewModels
{
    public class VerlanglijstViewModel : ViewModel
    {
        public VerlanglijstViewModel()
        {
            Producten = new List<Product>();
            StartDatum = DateTimeExtensie.StartOfWeek(DateTime.Now, DayOfWeek.Monday);
            EindDate = StartDatum.AddDays(7.0);
            
        }
        public virtual ICollection<Product> Producten { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDatum { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EindDate { get; set; }
        public int[] Aantallen { get; set; }
        public bool[] DagenLector { get; set; }
        
    }
}