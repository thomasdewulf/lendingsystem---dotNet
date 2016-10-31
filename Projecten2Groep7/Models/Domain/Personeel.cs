using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Linq;

namespace Projecten2Groep7.Models.Domain
{
    public class Personeel : ApplicationUser
    {
        List<ApplicationUser> hulpStudenten;
        List<ApplicationUser> studenten = new List<ApplicationUser>();
        int teller = 0;

        public override void VoegReservatieToe(Dictionary<Product, int> map, DateTime van, DateTime tot, Email[] email,bool[] dagen)
        {
            EmailService emailService = new EmailService();
            Blokkering b = new Blokkering
            {
                StartDatum = van,
                EindDatum = tot,
                ReservatieUser = this,
                ReservatieStatus = ReservatieStatus.Geblokkeerd,
                AanmaakDatum = DateTime.Now,
            };
            int i = 0;
            foreach (KeyValuePair<Product, int> entry in map)
            {
                if (entry.Value != 0)
                {
                    if(entry.Value > entry.Key.AantalInCatalogus)
                    {
                        throw new ArgumentOutOfRangeException("Er zijn niet genoeg stuks beschikbaar van " + entry.Key.Artikelnaam);
                    }if( entry.Value < 0)
                    {
                        throw new ArgumentOutOfRangeException("U kan geen negatieve waarde toekennen aan " + entry.Key.Artikelnaam);
                    }if(dagen == null || dagen.All(l => !l))
                    {
                        throw new ArgumentNullException("Selecteer 1 of meerdere dagen voor " + entry.Key.Artikelnaam);
                    }
                    int aantal = entry.Key.GeefAantalReserveerbaarInPeriode(van, tot);
                    if (aantal < entry.Value)
                    {
                        entry.Key.WijzigReservatieAantal(entry.Value - aantal)
                            .ForEach(s => addStudenten(s));    
                    }
                    bool[] dagenProduct = new bool[5];
                    Array.Copy(dagen, i, dagenProduct, 0, 5);
                    b.VoegReservatieLijnToe(entry.Key, entry.Value,dagenProduct);
                }
                i+=5;
            }
            Reservaties.Add(b);
            emailService.VerzendEmail(b, this, email[0]);

            studenten.Reverse();
            ApplicationUser[] array = studenten.ToArray();
            for (int j = 0; j < teller; j++)
            {
                if (j == 0)//Wijzigen
                    emailService.VerzendEmail(b, array[j], email[2]);
                else//anuleren
                    emailService.VerzendEmail(b, array[j], email[1]);
            }
        }

        private void addStudenten(ApplicationUser s)
        {
            if (!studenten.Contains(s))
            {
                studenten.Add(s);
                teller++;
            }
        }
    }
}