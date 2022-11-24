using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TravelSmart.Startup))]
namespace TravelSmart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
