using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PenDesign.WebUI.Startup))]
namespace PenDesign.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
