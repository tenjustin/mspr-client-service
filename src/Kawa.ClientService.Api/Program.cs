using Kawa.ClientService.Api.Models;
using Kawa.ClientService.Api.Services;
using Kawa.ClientService.ServiceDefaults.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ClientsDbContext>("client-service-db");
builder.AddRabbitMQClient("messaging");

// Ajouter les services de messagerie
builder.Services.AddScoped<IMessageBrokerService, MessageBrokerService>();
builder.Services.AddScoped<IMessageHandler<ClientMessage>, ClientMessageHandler>();
builder.Services.AddHostedService<MessageConsumerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
public partial class Program { }