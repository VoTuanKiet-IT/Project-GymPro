using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GymPro.Startup))]
namespace GymPro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
