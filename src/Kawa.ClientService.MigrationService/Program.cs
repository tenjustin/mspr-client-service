using Kawa.ClientService.ServiceDefaults.Database;
using SupportTicketApi.MigrationService;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddNpgsqlDbContext<ClientsDbContext>("client-service-db");

var host = builder.Build();
host.Run();
