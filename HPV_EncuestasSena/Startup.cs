using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HPV_EncuestasSena.Startup))]
namespace HPV_EncuestasSena
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
