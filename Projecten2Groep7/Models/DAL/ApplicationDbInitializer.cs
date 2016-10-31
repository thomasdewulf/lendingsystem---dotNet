using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Projecten2Groep7.Models.DAL
{
    public class ApplicationDbInitializer :DropCreateDatabaseAlways<ApplicationDbContext>
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        protected override void Seed(ApplicationDbContext context)
    {
            //userManager =
            //  HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            //roleManager =
            //   HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();

            // InitializeIdentity();
            var userStore = new UserStore<ApplicationUser>(context);
            userManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(context);
            roleManager = new RoleManager<IdentityRole>(roleStore);
            GebruikersEnRollenGenereren();
           
        base.Seed(context);
    }

        private void GebruikersEnRollenGenereren()
        {
           
            MaakGebruikerEnRollen("student@gebruiker.be", "P@ssword1", "student");
            MaakGebruikerEnRollen("lector@gebruiker.be","P@ssword1","lector");

           
        }

        private void MaakGebruikerEnRollen(string email, string wachtwoord, string rolNaam)
        {
            //Gebruiker aanmaken
            ApplicationUser gebruiker = userManager.FindByName(email);
            if(gebruiker == null)
            {
                gebruiker = new ApplicationUser { UserName = email, Email = email};
                IdentityResult resultaat = userManager.Create(gebruiker, wachtwoord);
                if(!resultaat.Succeeded)
                {
                    throw new ApplicationException(resultaat.Errors.ToString());
                }
            }


            //Rollen aanmaken
            IdentityRole rol = roleManager.FindByName(rolNaam);

            if(rol == null)
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