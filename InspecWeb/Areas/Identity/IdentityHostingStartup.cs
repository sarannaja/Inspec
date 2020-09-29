using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(InspecWeb.Areas.Identity.IdentityHostingStartup))]
namespace InspecWeb.Areas.Identity
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