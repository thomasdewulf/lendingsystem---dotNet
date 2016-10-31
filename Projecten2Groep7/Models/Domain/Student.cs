using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Projecten2Groep7.Models.Domain
{
    public class Student : ApplicationUser
    {
        public override void VoegReservatieToe(Dictionary<Product, int> map, DateTime startDate, DateTime eindDate, Email[] email,bool[] dagen = null)
        {
            EmailService service = new EmailService();
            Reservatie r = new Reservatie
            {
                StartDatum = startDate,
                EindDatum = eindDate,
                ReservatieStatus = ReservatieStatus.Gereserveerd,
                ReservatieUser = this,
                AanmaakDatum = DateTime.Now
            };
            foreach (KeyValuePair<Product, int> entry in map)
            {
                if (entry.Value != 0)
                {
                    if (entry.Value > entry.Key.GeefAantalReserveerbaarInPeriode(startDate, eindDate))
                    {
                        throw new ArgumentOutOfRangeException("Er zijn niet genoeg stuks beschikbaar van  " + entry.Key.Artikelnaam);
                    }
                    else if (entry.Value < 0)
                    {
                        throw new ArgumentOutOfRangeException("U kan geen negatieve waarde toekennen aan  " + entry.Key.Artikelnaam);
                    }
                    r.VoegReservatieLijnToe(entry.Key, entry.Value);
                }
            }
            Reservaties.Add(r);
            service.VerzendEmail(r,this,email[0]);
            
        }
    }
}