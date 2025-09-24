using Kawa.ClientService.ServiceDefaults.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;

namespace Kawa.ClientService.Api.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IDisposable
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
        .WithDatabase("testdb")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private readonly RabbitMqContainer _rabbitMq = new RabbitMqBuilder()
        .WithImage("rabbitmq:3.11")
        .WithUsername("guest")
        .WithPassword("guest")
        .Build();
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _postgres.StartAsync().GetAwaiter().GetResult();
        _rabbitMq.StartAsync().GetAwaiter().GetResult();

        base.ConfigureWebHost(builder);

        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<ClientsDbContext>));

            if (dbContextDescriptor != null)
            {
                services.Remove(dbContextDescriptor);
            }

            services.AddDbContext<ClientsDbContext>(options =>
            {
                options.UseNpgsql(_postgres.GetConnectionString());
            });

            var dbContext = services.BuildServiceProvider()
                .GetRequiredService<ClientsDbContext>();
            dbContext.Database.EnsureCreated();
        });
    }

    public void Dispose()
    {
        _postgres.DisposeAsync().GetAwaiter().GetResult();
    }
}