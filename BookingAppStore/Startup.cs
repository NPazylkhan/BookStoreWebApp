using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookingAppStore.Startup))]
namespace BookingAppStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
