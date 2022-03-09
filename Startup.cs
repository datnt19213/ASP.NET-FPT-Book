using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FPTBookProject.Startup))]
namespace FPTBookProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
