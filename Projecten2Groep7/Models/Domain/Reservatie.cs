using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projecten2Groep7.Models.Domain
{
    public class Reservatie
    {
        public Reservatie()
        {
            ReservatieLijnen = new List<ReservatieLijn>();   
        }

        [DisplayName("Start Datum")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDatum
        {
            get;
            set;
        }

        [DisplayName("Eind Datum")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EindDatum
        {
            get; set;
        }

        public DateTime AanmaakDatum { get; set; }
        public virtual ICollection<ReservatieLijn> ReservatieLijnen { get; set; }

        public ReservatieStatus ReservatieStatus { get; set; }
        [DisplayName("Reservatienummer")]
        public int ReservatieId { get; set; }

        public virtual ApplicationUser ReservatieUser { get; set; }

        public void VoegReservatieLijnToe(Product product , int aantal, bool[] dagen = null)
        {
            List<BlokkeringDag> dagenLijst = null;
            if (dagen != null)
            {
                dagenLijst = new List<BlokkeringDag>();
                for (int i = 0; i < dagen.Length; i++)
                {
                    if (dagen[i])
                    {
                        DateTime nieuwDatum = StartDatum;
                        nieuwDatum = nieuwDatum.AddDays(i);
                        dagenLijst.Add(new BlokkeringDag(nieuwDatum));
                    }
                }
            }
            ReservatieLijn rl = new ReservatieLijn()
            {
                Product = product,
                Aantal = aantal,
                Reservatie = this,
                Dagen = dagenLijst
            };
            product.VoegReservatieLijnToe(rl);
            ReservatieLijnen.Add(rl);
        }

        //public void KijkenVoorTeLaat()
        //{
        //    if (EindDatum < DateTime.Now && EindDatum.Hour > 17 && ReservatieStatus == ReservatieStatus.Afgehaald)
        //    {
        //        ReservatieStatus = ReservatieStatus.TeLaat;
        //    }
        //}
    }
}