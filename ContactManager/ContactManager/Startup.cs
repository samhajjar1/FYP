using Microsoft.Owin;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Owin;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

[assembly: OwinStartupAttribute(typeof(ContactManager.Startup))]
namespace ContactManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
