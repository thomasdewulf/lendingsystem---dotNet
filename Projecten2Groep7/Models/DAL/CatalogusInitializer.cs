using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Models.DAL
{
    public class CatalogusInitializer :  DropCreateDatabaseAlways<CatalogusContext>

    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        protected override void Seed(CatalogusContext context)
        {
            try
            {

                #region Creatie Doelgroep
                Doelgroep kleuterOnderwijs = new Doelgroep("Kleuter onderwijs") { DoelgroepId = 1 };
                Doelgroep lagerOnderwijs = new Doelgroep("Lager onderwijs") { DoelgroepId = 2 };
                Doelgroep secundairOnderwijs = new Doelgroep("Secundair onderwijs") { DoelgroepId = 3 };
                #endregion

                #region Creatie Leergebied
                Leergebied knutselen = new Leergebied("Knutselen") { LeergebiedId = 1 };
                Leergebied tekenen = new Leergebied("Tekenen") { LeergebiedId = 2 };
                Leergebied chemie = new Leergebied("Chemie") { LeergebiedId = 3 };
                #endregion

                #region Creatie Firma
                Firma Ruytcol = new Firma()
                {
                    FirmaId = 1,
                    Naam = "Ruytcol",
                    ContactPersoon = "Jof Ruytcol",
                    EmailContactPersoon = "jof.ruytcol@ruytcol.be",
                    Website = "www.ruytcol.be"
                };
                Firma Ledhaize = new Firma()
                {
                    FirmaId = 2,
                    Naam = "Ledhaize",
                    ContactPersoon = "Julien Ledhaize",
                    EmailContactPersoon = "julien.ledhaize@ledhaize.be",
                    Website = "www.ledhaize.be"
                }; 
                #endregion

                #region Creatie Product
                Product dobbelsteen = new Product()
                {
                    Artikelnaam = "dobbelsteen schatkist",
                    Omschrijving = "Koffertje met verschillende dobbelstenen : blanco met cijfers,... 162-delig.",
                    Artikelnummer = "MH1447",
                    Prijs = 35,
                    AantalInCatalogus = 10,
                    Uitleenbaar = true,
                    Foto = "~/Content/images/dobbelsteen.jpg",
                    ProductId = 1,
                    AantalProductStukken = 5,
                    Firma = Ruytcol
                };
                dobbelsteen.Doelgroepen.Add(kleuterOnderwijs);
                dobbelsteen.Leergebieden.Add(knutselen);

                Product draaischijf = new Product()
                {
                    Artikelnaam = "Blanco draaischijf",
                    Omschrijving = "Met verschillende blanco",
                    Artikelnummer = "EL5955",
                    Prijs = 31.45M,
                    AantalInCatalogus = 15,
                    Uitleenbaar = false,
                    Foto = "~/Content/images/draaischijf.jpg",
                    ProductId = 2,
                    AantalProductStukken = 1,
                    Firma = Ledhaize
                };


                draaischijf.Doelgroepen.Add(lagerOnderwijs);
                draaischijf.Doelgroepen.Add(secundairOnderwijs);
                draaischijf.Leergebieden.Add(tekenen);

                Product dissectiebak = new Product()
                {
                    Artikelnaam = "Dissectiebakken",
                    Omschrijving = "Bakken voor het dissecteren.",
                    Artikelnummer = "DB6987",
                    Prijs = 15,
                    AantalInCatalogus = 15,
                    Uitleenbaar = true,
                    Foto = "~/Content/images/dissectiebak.jpg",
                    ProductId = 3,
                    AantalProductStukken = 5,
                    Firma = Ledhaize
                };

                dissectiebak.Doelgroepen.Add(lagerOnderwijs);
                dissectiebak.Doelgroepen.Add(secundairOnderwijs);
                dissectiebak.Leergebieden.Add(chemie);

                Product verfborstel = new Product()
                {
                    Artikelnaam = "Verfborstel",
                    Omschrijving = "Kwast voor het verfen van fijne lijnen.",
                    Artikelnummer = "VB4200",
                    Prijs = 5,
                    AantalInCatalogus = 20,
                    Uitleenbaar = false,
                    Foto = "~/Content/images/verfborstel.jpg",
                    ProductId = 4,
                    AantalProductStukken = 3,
                    Firma = Ruytcol
                };
                verfborstel.Doelgroepen.Add(kleuterOnderwijs);
                verfborstel.Doelgroepen.Add(lagerOnderwijs);
                verfborstel.Doelgroepen.Add(secundairOnderwijs);
                verfborstel.Leergebieden.Add(knutselen);
                verfborstel.Leergebieden.Add(tekenen);
                #endregion

                #region Creatie Gebruikers en Rollen

                var userStore = new UserStore<ApplicationUser>(context);
                userManager = new UserManager<ApplicationUser>(userStore);
                var roleStore = new RoleStore<IdentityRole>(context);
                roleManager = new RoleManager<IdentityRole>(roleStore);
                GebruikersEnRollenGenereren();

                #endregion

                String headerReservatie = "Beste {voornaam} {naam}, <br/> Uw reservatie is succesvol geregistreerd. U vindt de details van de reservatie onderaan.<br/>";
                String bodyReservatie = "<br /> Start datum reservatie: {reservatieStartDatum}";
                bodyReservatie += "<br /><br /> Eind datum reservatie: {reservatieEindDatum}";
                String footerReservatie = "<br /><br /> Met vriendelijke groeten, ";
                footerReservatie += "<br /><br /> Het team Didactische Leermiddelen Groep 7";
                String subjectReservatie = "Bevestiging Reservatie van {reservatieStartDatum} tot  {reservatieEindDatum}";
                Email reservatieEmail = new Email(headerReservatie, bodyReservatie, footerReservatie, subjectReservatie, ReservatieStatus.Gereserveerd);
                Email blokkeringEmail = new Email(headerReservatie, bodyReservatie, footerReservatie, subjectReservatie, ReservatieStatus.Geblokkeerd);
                String headerWijziging = "Beste {voornaam} {naam}, <br/> Uw reservatie is gewijzigd. U vindt de details van de wijziging onderaan.<br/>";
                String bodyWijziging = "<br /> Start datum reservatie: {reservatieStartDatum}";
                bodyWijziging += "<br /><br /> Eind datum reservatie: {reservatieEindDatum}";
                String footerWijziging = "<br /><br /> Met vriendelijke groeten, ";
                footerWijziging += "<br /><br /> Het team Didactische Leermiddelen Groep 7";
                String subjectWijziging = "Wijziging reservatie van {reservatieStartDatum} tot  {reservatieEindDatum}";
                Email wijzigEmail = new Email(headerWijziging, bodyWijziging, footerWijziging, subjectWijziging, ReservatieStatus.Gewijzigd);
                String headerAnnulering = "Beste {voornaam} {naam}, <br/> Uw reservatie is geannuleerd. U vindt de details van de annulatie onderaan.<br/>";
                String bodyAnnulering = "<br /> Start datum reservatie: {reservatieStartDatum}";
                bodyAnnulering += "<br /><br /> Eind datum reservatie: {reservatieEindDatum}";
                String footerAnnulering = "<br /><br /> Met vriendelijke groeten, ";
                footerAnnulering += "<br /><br /> Het team Didactische Leermiddelen Groep 7";
                String subjectAnnulering = "Annulering reservatie van {reservatieStartDatum} tot  {reservatieEindDatum}";
                Email annuleringEmail = new Email(headerAnnulering, bodyAnnulering, footerAnnulering, subjectAnnulering, ReservatieStatus.Geannuleerd);
                

                #region Context add
                context.Doelgroepen.Add(kleuterOnderwijs);
                context.Doelgroepen.Add(lagerOnderwijs);
                context.Doelgroepen.Add(secundairOnderwijs);
                context.Leergebieden.Add(knutselen);
                context.Leergebieden.Add(tekenen);
                context.Leergebieden.Add(chemie);
                context.Producten.Add(draaischijf);
                context.Producten.Add(dobbelsteen);
                context.Producten.Add(dissectiebak);
                context.Producten.Add(verfborstel);
                context.Emails.Add(reservatieEmail);
                context.Emails.Add(blokkeringEmail);
                context.Emails.Add(wijzigEmail);
                context.Emails.Add(annuleringEmail);
                #endregion

                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string s = "Fout creatie database ";
                foreach (var eve in e.EntityValidationErrors)
                {
                    s += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.GetValidationResult());
                    foreach (var ve in eve.ValidationErrors)
                    {
                        s += String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                } throw new Exception(s);
            }
        }
        private void GebruikersEnRollenGenereren()
        {

            MaakGebruikerEnRollen("student@gebruiker.be", "P@ssword1", "student",1,"Josken","Vermeulen");
            MaakGebruikerEnRollen("lector@gebruiker.be", "P@ssword1", "personeel",2,"Ollie","Strikkert");

        }

        private void MaakGebruikerEnRollen(string email, string wachtwoord, string rolNaam,int nr,string voornaam,string achternaam)
        {
            //Gebruiker aanmaken
            ApplicationUser gebruiker = userManager.FindByName(email);

            if (gebruiker == null)
            {
                Verlanglijst verlanglijst = new Verlanglijst(){VerlanglijstId = nr};
                if (rolNaam == "student")
                {
                    gebruiker = new Student { Voornaam = voornaam, Naam = achternaam, GebruikersNummer = email, UserName = email, Email = email, Verlanglijst = verlanglijst, Reservaties = new List<Reservatie>() };
                }
                else
                {
                    gebruiker = new Personeel { Voornaam = voornaam, Naam = achternaam, GebruikersNummer = email, UserName = email, Email = email, Verlanglijst = verlanglijst, Reservaties = new List<Reservatie>() };
                }                
                IdentityResult resultaat = userManager.Create(gebruiker, wachtwoord);
                
                
                if (!resultaat.Succeeded)
                {
                    throw new ApplicationException(resultaat.Errors.ToString());
                }
                
            }

            //Rollen aanmaken
            IdentityRole rol = roleManager.FindByName(rolNaam);

            if (rol == null)
            {
                rol = new IdentityRole(rolNaam);
                IdentityResult resultaat = roleManager.Create(rol);
                if (!resultaat.Succeeded)
                {
                    throw new ApplicationException(resultaat.Errors.ToString());
                }

            }

            //Rol toekennen aan aangemaakte gebruiker
            IList<String> rollenVanGebruiker = userManager.GetRoles(gebruiker.Id);
            if (!rollenVanGebruiker.Contains(rol.Name))
            {
                IdentityResult resultaat = userManager.AddToRole(gebruiker.Id, rolNaam);
                if (!resultaat.Succeeded)
                    throw new ApplicationException(resultaat.Errors.ToString());

            }
        }
    }
}