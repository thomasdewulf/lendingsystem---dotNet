using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace Projecten2Groep7.Models.Domain
{
    public class Blokkering : Reservatie
    {
        public Blokkering()
        {
            base.ReservatieLijnen = new List<ReservatieLijn>();
        }
    }

    
}