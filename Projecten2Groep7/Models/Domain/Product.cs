using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebGrease.Css.Extensions;

namespace Projecten2Groep7.Models.Domain
{
    public class Product
    {
        #region Properties
        public Product()
        {
            Leergebieden = new HashSet<Leergebied>();
            Doelgroepen = new HashSet<Doelgroep>();
            ReservatieLijnen = new List<ReservatieLijn>();
        }

        public int ProductId { get; set; }

        [Display(Name = "Foto")]
        public virtual string Foto
        {
            get;
            set;
        }

        [Display(Name = "Artikelnaam")]
        public virtual string Artikelnaam
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

        [Display(Name = "Artikelnummer")]
        public string Artikelnummer
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
        [Display(Name = "Aantal")]
        public int AantalInCatalogus
        {
            get;
            set;
        }
        public virtual Firma Firma
        {
            get;
            set;
        }

        [Display(Name = "Doelgroepen")]
        public virtual ICollection<Doelgroep> Doelgroepen
        {
            get;
            set;
        }

        [Display(Name = "Leergebieden")]
        public virtual ICollection<Leergebied> Leergebieden
        {
            get;
            set;
        }
        public virtual ICollection<ReservatieLijn> ReservatieLijnen { get; set; }

        [Display(Name = "Uitleenbaar")]
        public bool Uitleenbaar
        {
            get;
            set;
        }
        
        [Display(Name = "Aantal Stuk")]
        public int AantalProductStukken { get; set; }

        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            if (obj != null && obj is Product)
                if ((obj as Product).ProductId.Equals(ProductId))
                    return true;
            return false;
        }

        public override int GetHashCode()
        {
            return ProductId;
        } 
        #endregion

        public bool BevatDoelgroep(string naam)
        {
            return Doelgroepen.Any(doelgroep => doelgroep.Naam.ToLower().Contains(naam.ToLower()));
        }
        public bool BevatLeergebied(string naam)
        {
            return Leergebieden.Any(leergebied => leergebied.Naam.ToLower().Contains(naam.ToLower()));
        }

        public int GeefAantalReserveerbaarInPeriode(DateTime startDatum, DateTime eindDatum)
        {
            int aantal = AantalInCatalogus;
            ReservatieLijnen.ForEach(rl =>
            {
                if (startDatum == rl.GeefStartDatumVoorReservatie() && eindDatum == rl.GeefEindDatumVoorReservatie())
                    aantal -= rl.Aantal;
            });
            return aantal;
        }
        public void VoegReservatieLijnToe(ReservatieLijn reservatieLijn)
        {
            ReservatieLijnen.Add(reservatieLijn);
        }

        public List<ApplicationUser> WijzigReservatieAantal(int aantal)
        {
            List<ApplicationUser> hulp = new List<ApplicationUser>();
            List<ReservatieLijn> sortedReservatieLijnen = ReservatieLijnen.OrderByDescending(rl => rl.GeefAanmaakDatumVoorReservatie()).ToList();
            foreach (var reslijn in sortedReservatieLijnen)
            {
                if (reslijn.Reservatie.ReservatieUser is Student)
                {
                    if (reslijn.Aantal == aantal)
                    {
                        reslijn.Aantal = 0;
                        reslijn.Reservatie.ReservatieStatus = ReservatieStatus.Geannuleerd;
                        if (!hulp.Contains(reslijn.Reservatie.ReservatieUser))
                            hulp.Add(reslijn.Reservatie.ReservatieUser);
                    }
                    else if (aantal > reslijn.Aantal)
                    {
                        aantal -= reslijn.Aantal;
                        reslijn.Reservatie.ReservatieStatus = ReservatieStatus.Geannuleerd;
                        reslijn.Aantal = 0;
                        if (!hulp.Contains(reslijn.Reservatie.ReservatieUser))
                            hulp.Add(reslijn.Reservatie.ReservatieUser);
                    }
                    else
                    {
                        reslijn.Aantal -= aantal;
                        reslijn.Reservatie.ReservatieStatus = ReservatieStatus.Gewijzigd;
                        if(!hulp.Contains(reslijn.Reservatie.ReservatieUser))
                            hulp.Add(reslijn.Reservatie.ReservatieUser);
                    }
                }
            }
            return hulp;
        }

        public ICollection<Reservatie> GeefAlleReservatiesMetStatusGeBlokkeerdEnGereserveerd()
        {
            List<Reservatie> reservaties = new List<Reservatie>();
            foreach(var reservatieLijn in ReservatieLijnen)
            {
                reservaties.Add(reservatieLijn.Reservatie);
            }
            return reservaties;
        }
    }

    
}
