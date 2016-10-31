using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Projecten2Groep7.Models;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Controllers
{
    [Authorize]
    public class ReservatieController : Controller
    {
        // GET: Reservatie
        public ActionResult Index(ApplicationUser user)
        {
            if (user.Reservaties.Count == 0)
                return View("LegeReservaties");
            ICollection<ReservatieLijn> lijnen = new List<ReservatieLijn>();
            ICollection<Reservatie> reservaties = user.Reservaties.OrderBy(r => r.StartDatum)
                .Where(r => r.ReservatieStatus != ReservatieStatus.Afgehaald)
                .ToList();
            foreach (var reservatie in reservaties)
            {
                foreach (var lijn in reservatie.ReservatieLijnen)
                {
                    lijnen.Add(lijn);
                }
            }
            return View(lijnen);
        }
    }
}