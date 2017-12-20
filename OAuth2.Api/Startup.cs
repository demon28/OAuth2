using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OAuth2.Api.Startup))]
namespace OAuth2.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
