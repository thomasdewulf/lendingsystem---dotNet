using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projecten2Groep7.Models.Domain
{
    public class Doelgroep
    {
        #region Properties
        public string Naam { get; private set; }
        public int DoelgroepId { get; set; }
        #endregion
        #region Methods
        public Doelgroep(string naam)
        {
            this.Naam = naam;
        }

        public Doelgroep()
        {
            
        }
        #endregion




    }
}