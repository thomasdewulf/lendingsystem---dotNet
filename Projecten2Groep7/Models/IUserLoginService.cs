using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity; // Maybe this one too
using Microsoft.AspNet.Identity.Owin;
using Projecten2Groep7.Models;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Models
{
   public interface IUserLoginService
   {
    Task<SignInStatus> Login(String email, String wachtwoord, bool herinnerme);

       

   }

    public class TestLoginService : IUserLoginService
    {
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        public TestLoginService(ApplicationSignInManager signInManager, ApplicationUserManager userManager)
        {
            SignInManager = signInManager;
            UserManager = userManager;
        }
        public ApplicationSignInManager SignInManager { get;private set; }

        public ApplicationUserManager UserManager { get; set; }
        public async Task<SignInStatus> Login(string email, string wachtwoord, bool herinnerme)
        {
            return await SignInManager.PasswordSignInAsync(email, wachtwoord, herinnerme, shouldLockout: false);
        }
    }

    public class HogentLoginService : IUserLoginService
    {
        public ApplicationSignInManager SignInManager { get; private set; }

        public ApplicationUserManager UserManager { get; set; }

        public HogentLoginService(ApplicationSignInManager signInManager, ApplicationUserManager userManager)
        {
            SignInManager = signInManager;
            UserManager = userManager;
        }

        public async Task<SignInStatus> Login(string email, string wachtwoord, bool herinnerme)
        {
            dynamic userInfo = this.parseJson(email, wachtwoord);
         
            if (userInfo != null)
            {
                ApplicationUser gebruiker = UserManager.FindByEmail(email);

                if (gebruiker == null)
                {
                    var result = await    this.MaakApplicationUser(email,userInfo,wachtwoord);
                }
                else
                {
                    this.UpdateWachtwoord(gebruiker, wachtwoord);
                }
                //Task<ApplicationUser> gebruiker = await UserManager.FindByNameAsync(email) ?? this.MaakApplicationUser(userInfo);
                return await SignInManager.PasswordSignInAsync(email, wachtwoord, herinnerme, shouldLockout: false);
            }
   
            
            return SignInStatus.Failure;
        }

        private async void UpdateWachtwoord(ApplicationUser gebruiker, string wachtwoord)
        {
            gebruiker.PasswordHash = UserManager.PasswordHasher.HashPassword(wachtwoord);
            var result = await UserManager.UpdateAsync(gebruiker);
        }


        private async Task<IdentityResult> MaakApplicationUser(string userName,dynamic userInfo,string wachtwoord)
        {
            ApplicationUser user = null;
            if (userInfo[3] == "student")
            {
                user = new Student()
                {
                    UserName = userName,
                    Email = userInfo[5],
                    GebruikersNummer = userName,
                    Naam = userInfo[1],
                    Voornaam = userInfo[4],
                    Verlanglijst = new Verlanglijst() { VerlanglijstId = (UserManager.Users.Count() + 1) },
                    Reservaties = new List<Reservatie>(),
                    Foto = userInfo[2]
                };
            }
            else
            {
                user = new Personeel
                {
                    UserName = userName,
                    Email = userInfo[5],
                    GebruikersNummer = userName,
                    Naam = userInfo[1],
                    Voornaam = userInfo[4],
                    Verlanglijst = new Verlanglijst() {VerlanglijstId = (UserManager.Users.Count() + 1)},
                    Reservaties = new List<Reservatie>(),
                    Foto = userInfo[2]

                };
            }
            var resultaat = await UserManager.CreateAsync(user, wachtwoord);
            var roleResult = await UserManager.AddToRoleAsync(user.Id, userInfo[3]);
            return resultaat;
        }

        private dynamic[] parseJson(string userName, string wachtwoord)
        {
            string wachtwoordHash = this.hashPassword(wachtwoord);
            var json =
                new WebClient().DownloadString("https://studservice.hogent.be/auth/" + userName + "/" +
                                               wachtwoordHash);
            if (json == "\"[]\"")
            {
                return null;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic jsonObject = serializer.Deserialize<dynamic>(json);

            dynamic faculteit = jsonObject["FACULTEIT"];
            dynamic naam = jsonObject["NAAM"];
            dynamic foto = jsonObject["BASE64FOTO"];
            string type = jsonObject["TYPE"];
            dynamic voornaam = jsonObject["VOORNAAM"];
            dynamic email = jsonObject["EMAIL"];

            dynamic[] gegevens = new dynamic[6] { faculteit, naam, foto, type, voornaam, email };

            return gegevens;
        }

        private string hashPassword(string password)
        {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
