using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;

namespace Projecten2Groep7.Models
{
    public interface ILoginServiceFactory
    {
        IUserLoginService GetLoginService(string Email);
    }

    public class UserLoginServiceFactory : ILoginServiceFactory
    {
        private readonly IUserLoginService _testGebruikerLogin;
        private readonly IUserLoginService _hogentGebruikerLogin;

        public UserLoginServiceFactory([Named("TestLogin")] IUserLoginService testLoginService,
            [Named("HogentLogin")] IUserLoginService hogentLoginService)
        {
            _testGebruikerLogin = testLoginService;
            _hogentGebruikerLogin = hogentLoginService;
        }

        public IUserLoginService GetLoginService(string email)
        {
            if (email.Contains("@gebruiker.be"))
            {
                return _testGebruikerLogin;
            }
            return _hogentGebruikerLogin;
        }
    }
}