using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projecten2Groep7.Models.Domain
{
    public class BlokkeringDag
    {
        public int BlokkeringDagId { get; set; }
        public DateTime Dag { get; set; }

        public BlokkeringDag(DateTime dag)
        {
            Dag = dag;
        }
    }
}