using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GarbCollector.Startup))]
namespace GarbCollector
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
