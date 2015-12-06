using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RISH.Startup))]
namespace RISH
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
