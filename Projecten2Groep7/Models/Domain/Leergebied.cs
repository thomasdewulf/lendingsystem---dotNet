using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projecten2Groep7.Models.Domain
{
    public class Leergebied
    {
        #region Properties
        public string Naam { get; private set; }
        public int LeergebiedId { get;set;}
        #endregion

        #region Methods
        public Leergebied(string naam)
        {
            this.Naam = naam;
        }

        public Leergebied()
        {
                
        }
        #endregion
    }
}