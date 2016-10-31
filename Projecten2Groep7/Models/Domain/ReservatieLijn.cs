using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace Projecten2Groep7.Models.Domain
{
    public class ReservatieLijn
    {
        //private int aantal;
        public int ReservatieLijnId
        {
            get; set;
        }

        public virtual Product Product
        {
            get; set;
        }

        public virtual int Aantal { get; set;}

        public virtual Reservatie Reservatie { get; set; }

        public ICollection<BlokkeringDag> Dagen { get; set; }

        public virtual DateTime GeefStartDatumVoorReservatie()
        {
            return Reservatie.StartDatum;
        }
        public virtual DateTime GeefEindDatumVoorReservatie()
        {
            return Reservatie.EindDatum;
        }

        public ApplicationUser GeefReservatieUser()
        {
            return Reservatie.ReservatieUser;
        }

        public DateTime GeefAanmaakDatumVoorReservatie()
        {
            return Reservatie.AanmaakDatum;
        }
    }
}