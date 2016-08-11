using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tweet_Wall.Startup))]
namespace Tweet_Wall
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
