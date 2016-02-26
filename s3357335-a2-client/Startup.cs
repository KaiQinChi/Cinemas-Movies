using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(s3357335_a2_client.Startup))]

namespace s3357335_a2_client
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}