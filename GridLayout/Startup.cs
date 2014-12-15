using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GridLayout.Startup))]
namespace GridLayout
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
