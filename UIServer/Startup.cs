using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UIServer.Startup))]
namespace UIServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            var config = new HubConfiguration()
            {
                EnableDetailedErrors = true
            };
            app.MapSignalR(config);
        }
    }
}
