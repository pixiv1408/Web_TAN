using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web_TAN.Startup))]
namespace Web_TAN
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
