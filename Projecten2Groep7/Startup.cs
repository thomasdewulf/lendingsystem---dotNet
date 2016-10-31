using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projecten2Groep7.Startup))]
namespace Projecten2Groep7
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
