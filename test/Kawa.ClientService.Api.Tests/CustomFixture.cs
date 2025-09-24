using System;
using Kawa.ClientService.ServiceDefaults.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Kawa.ClientService.Api.Tests;

public class CustomFixture : IClassFixture<CustomWebApplicationFactory>, IAsyncDisposable
{
    private readonly CustomWebApplicationFactory _factory;

    public CustomFixture()
    {
        _factory = new CustomWebApplicationFactory();
        Client = _factory.CreateClient();
        Seeder = new DbTestSeeder(_factory.Services);
    }

    public HttpClient Client { get; set; }

    public DbTestSeeder Seeder { get; set; }

    public async ValueTask DisposeAsync()
    {
        await _factory.DisposeAsync();
    }
}
