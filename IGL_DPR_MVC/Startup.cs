using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IGL_DPR_MVC.Startup))]
namespace IGL_DPR_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
