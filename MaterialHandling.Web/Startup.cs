using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MaterialHandling.Web.Startup))]
namespace MaterialHandling.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
        }
    }
}
