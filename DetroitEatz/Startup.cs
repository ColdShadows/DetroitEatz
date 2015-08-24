using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DetroitEatz.Startup))]
namespace DetroitEatz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
