using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SightsView.Web.Areas.Identity.IdentityHostingStartup))]

namespace SightsView.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
