using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Names.Edit.MvcSite.Startup))]
namespace Names.Edit.MvcSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
