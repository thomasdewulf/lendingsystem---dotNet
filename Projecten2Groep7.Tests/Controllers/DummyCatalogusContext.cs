using Projecten2Groep7.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Projecten2Groep7.Models;

namespace Projecten2Groep7.Tests.Controllers
{
    class DummyCatalogusContext
    {
        public IQueryable<Doelgroep> Doelgroepen { get; private set; }
        public IQueryable<Leergebied> Leergebieden { get; private set; }
        public IQueryable<Product> Producten { get; private set; }
        public IQueryable<ApplicationUser> Gebruikers { get; private set; }
        public Product Schatkist { get; private set; }
        public Product Draaischijf { get; private set; }
        public Product Splitsbomen { get; private set; }
        public Leergebied Kind { get; private set; }
        public Doelgroep LagerOnderwijs { get; private set; }
        public ApplicationUser CurrentGebruiker{ get; private set; }
        public ApplicationUser CurrentGebruikerPersoneel { get; private set; }
        public Email Email { get; private set; }


        public DummyCatalogusContext()
        {
            LagerOnderwijs = new Doelgroep("Lager onderwijs");
            Kind = new Leergebied("Kind");
            Doelgroepen = (new Doelgroep[] { LagerOnderwijs }).ToList().AsQueryable();
            Leergebieden = (new Leergebied[] { Kind }).ToList().AsQueryable();

            string omschrijvingSchatkist = "koffertje met verschillende soorten dobbelstenen: blanco, met cijfers, ...";
            string omschrijvingDraaischijf = "met verschillende blanco schijven in hard papier";
            string omschrijvingSplitbomen = "aan de hand van rode bolletjes kunnen getallen tot 10,"
                + "in de stam van de boom gesplitst worden in 2 getallen (kaartjes) of in 2 x aantal bolletjes (boom)";

            Schatkist = new Product
            {
                Artikelnaam = "Schatkist",
                Omschrijving = omschrijvingSchatkist,
                Artikelnummer = "MH1447",
                Prijs = 35,
                AantalInCatalogus = 1,
                Uitleenbaar = true,
                ProductId = 1,
                Doelgroepen = Doelgroepen.ToList(),
                Leergebieden = Leergebieden.ToList()
            };
            Draaischijf = new Product
            {
                Artikelnaam = "Draaischijf",
                Omschrijving = omschrijvingDraaischijf,
                Artikelnummer = "EL5955",
                Prijs = Convert.ToDecimal(31.45),
                AantalInCatalogus = 1,
                Uitleenbaar = true,
                ProductId = 2,
                Doelgroepen = Doelgroepen.ToList(),
                Leergebieden = Leergebieden.ToList()
            };
            
            Splitsbomen = new Product()
            {
                AantalInCatalogus = 5,
                Artikelnaam = "Splitsbomen",
                Artikelnummer = "RK2367",
                Doelgroepen = Doelgroepen.ToList(),
                Leergebieden = Leergebieden.ToList(),
                Omschrijving = omschrijvingSplitbomen,
                Prijs = Convert.ToDecimal(2.9),
                ProductId = 3,
                Uitleenbaar = false

            };
            Producten = (new Product[] { Schatkist, Draaischijf, Splitsbomen}).ToList().AsQueryable().OrderBy(p => p.Artikelnaam);
            Gebruikers = (new ApplicationUser[]{CurrentGebruiker}).ToList().AsQueryable();
            CurrentGebruiker = new Student()//TODO
            {
                UserName = "test.gebruiker@gebruiker.be",
                GebruikersNummer = "1",
                Email = "test.gebruiker@gebruiker.be",
                Naam = "testNaam",
                Voornaam = "testVoornaam",
                Verlanglijst = new Verlanglijst(),
                Reservaties = new List<Reservatie>()
            };
            CurrentGebruikerPersoneel = new Personeel()
            {
                UserName = "test.personeel@gebruiker.be",
                GebruikersNummer = "2",
                Email = "test.personeel@gebruiker.be",
                Naam = "testPersoneel",
                Voornaam = "testPersoneelV",
                Verlanglijst = new Verlanglijst(),
                Reservaties = new List<Reservatie>()
            };
            Email = new Email()
            {
                Body = "body",
                EmailId = 1,
                Footer = "footer",
                Header = "header",
                Status = ReservatieStatus.Gereserveerd,
                Subject = "subject"
            };
        }

        public Product GetProduct(int id)
        {
            return Producten.FirstOrDefault(p => p.ProductId == id);
        }
    }
}
