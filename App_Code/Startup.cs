using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(uzduotis1.Startup))]
namespace uzduotis1
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
