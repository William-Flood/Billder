using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Billder.Startup))]
namespace Billder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
