using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppBiblioteca.Startup))]
namespace AppBiblioteca
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
