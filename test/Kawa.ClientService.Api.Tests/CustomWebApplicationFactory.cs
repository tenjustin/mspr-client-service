using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Kawa.ClientService.Api.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // You can configure the web host here if needed
        // For example, you can set up test-specific configurations or services
        base.ConfigureWebHost(builder);
    }
}