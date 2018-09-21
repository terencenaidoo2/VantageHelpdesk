using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VantageHelpdesk.Startup))]
namespace VantageHelpdesk
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
