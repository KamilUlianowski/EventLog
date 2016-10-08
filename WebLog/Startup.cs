using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebLog.Startup))]
namespace WebLog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
