using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SystemKurierskiPracaInzynierska.Startup))]
namespace SystemKurierskiPracaInzynierska
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
