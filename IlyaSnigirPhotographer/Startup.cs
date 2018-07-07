using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IlyaSnigirPhotographer.Startup))]
namespace IlyaSnigirPhotographer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
