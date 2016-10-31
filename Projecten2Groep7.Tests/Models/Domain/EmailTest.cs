using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Tests.Models.Domain
{
    [TestClass]
    public class EmailTest
    {
        private Email email;
        private Email emailConstructor;

        [TestInitialize]
        public void Initialize()
        {
            email = new Email()
            {
                Body = "body",
                EmailId = 1,
                Footer = "footer",
                Header = "header",
                Status = ReservatieStatus.Gereserveerd,
                Subject = "onderwerp"
            };
            emailConstructor = new Email("header", "body", "footer", "onderwerp", ReservatieStatus.Gereserveerd);
        }
        [TestMethod]
        public void EmailWordtCorrectAangemaakt()
        {
            Assert.IsNotNull(email);
            Assert.AreEqual("body", email.Body);
            Assert.AreEqual(1, email.EmailId);
            Assert.AreEqual("footer", email.Footer);
            Assert.AreEqual("header", email.Header);
            Assert.AreEqual(ReservatieStatus.Gereserveerd, email.Status);
            Assert.AreEqual("onderwerp", email.Subject);
        }
        [TestMethod]
        public void EmailWordtCorrectAangemaaktMetConstructor()
        {
            Assert.IsNotNull(emailConstructor);
            Assert.AreEqual("body", emailConstructor.Body);
            Assert.AreEqual("footer", emailConstructor.Footer);
            Assert.AreEqual("header", emailConstructor.Header);
            Assert.AreEqual(ReservatieStatus.Gereserveerd, emailConstructor.Status);
            Assert.AreEqual("onderwerp", emailConstructor.Subject);
        }
    }
}
