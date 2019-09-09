using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Names.Read.MvcSite.Startup))]
namespace Names.Read.MvcSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
