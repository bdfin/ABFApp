using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ABF.Startup))]
namespace ABF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
