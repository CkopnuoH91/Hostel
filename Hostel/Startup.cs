using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hostel.Startup))]
namespace Hostel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
