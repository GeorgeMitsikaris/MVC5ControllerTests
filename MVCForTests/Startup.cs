using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCForTests.Startup))]
namespace MVCForTests
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
