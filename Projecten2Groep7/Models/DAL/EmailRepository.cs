using Projecten2Groep7.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Projecten2Groep7.Models.DAL
{
    public class EmailRepository : IEmailRepository
    {
        private CatalogusContext context;
        private readonly DbSet<Email> emails;
        public EmailRepository(CatalogusContext context)
        {
            this.context = context;
            this.emails = context.Emails;
        }
        public Email FindByReservatieStatus(ReservatieStatus status)
        {
            return emails.FirstOrDefault(e => e.Status == status);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}