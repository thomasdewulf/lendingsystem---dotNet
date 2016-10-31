using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Projecten2Groep7.Models.Domain
{
    public class EmailService
    {
        public void VerzendEmail(Reservatie reservatie, ApplicationUser user, Email email)
        {
            VerzendBevestigingReservatie(reservatie, user, email);   
        }
        private void VerzendBevestigingReservatie(Reservatie reservatie, ApplicationUser user, Email email)
        {
            //Code werkt, er is alleen een smtp server nodig

            MailMessage mm = new MailMessage("didactischeleermiddelend@gmail.com", user.Email);
            mm.Subject = email.Subject.Replace("{reservatieStartDatum}", reservatie.StartDatum.ToString("D")).Replace("{reservatieEindDatum}", reservatie.EindDatum.ToString("D"));
            mm.Body = email.Header + email.Body + email.Footer;
            mm.Body = mm.Body.Replace("{naam}", user.Naam).Replace("{voornaam}", user.Voornaam).Replace("{reservatieStartDatum}", reservatie.StartDatum.ToString("D")).Replace("{reservatieEindDatum}", reservatie.EindDatum.ToString("D"));
            mm.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("didactischeleermiddelen@gmail.com", "P@ssword12");
            smtp.Send(mm);
        }
    }
}