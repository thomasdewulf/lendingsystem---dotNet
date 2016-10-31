using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projecten2Groep7.Models.Domain
{
    public interface IEmailRepository
    {
       void SaveChanges();
       Email FindByReservatieStatus(ReservatieStatus status);
    }
}