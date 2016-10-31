using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Tests.Models.Domain
{
    [TestClass]
    public class BlokkeringDagTest
    {
        private BlokkeringDag blokkeringDag;
        private DateTime dag;

        [TestInitialize]
        public void Initialize()
        {
            dag = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            blokkeringDag = new BlokkeringDag(dag);
            blokkeringDag.BlokkeringDagId = 2;
        }
        [TestMethod]
        public void BlokkeringDagWordtCorrectAangemaakt()
        {
            Assert.IsNotNull(blokkeringDag);
            Assert.AreEqual(dag,blokkeringDag.Dag);
            Assert.AreEqual(2, blokkeringDag.BlokkeringDagId);
        }
    }
}
