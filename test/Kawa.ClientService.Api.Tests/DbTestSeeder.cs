using System;
using Kawa.ClientService.ServiceDefaults.Database;
using Kawa.ClientService.ServiceDefaults.Database.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Kawa.ClientService.Api.Tests;

public class DbTestSeeder
{
    private readonly IServiceProvider _serviceProvider;

    public DbTestSeeder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void AddTestClients()
    {
        using var scope = _serviceProvider.CreateScope();
        var _context = scope.ServiceProvider.GetRequiredService<ClientsDbContext>();
        // Ajouter des clients de test
        var clients = new List<Client>
        {
            new Client { Id = Guid.NewGuid().ToString(), Nom = "Client A", Email = "clienta@example.com", Prenom = "PrenomA", Username = "clienta", CreatedAt = DateTime.UtcNow, CodePostal = "75001", Ville = "Paris", Societe = "SocieteA" },
            new Client { Id = Guid.NewGuid().ToString(), Nom = "Client B", Email = "clientb@example.com", Prenom = "PrenomB", Username = "clientb", CreatedAt = DateTime.UtcNow, CodePostal = "75002", Ville = "Lyon", Societe = "SocieteB" },
            new Client { Id = Guid.NewGuid().ToString(), Nom = "Client C", Email = "clientc@example.com", Prenom = "PrenomC", Username = "clientc", CreatedAt = DateTime.UtcNow, CodePostal = "75003", Ville = "Marseille", Societe = "SocieteC" }
        };

        _context.Clients.AddRange(clients);
        _context.SaveChanges();
    }
}
