using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StdBlog.Startup))]
namespace StdBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
