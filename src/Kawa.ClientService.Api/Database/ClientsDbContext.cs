using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Kawa.ClientService.ServiceDefaults.Database.Models;

namespace Kawa.ClientService.ServiceDefaults.Database
{


    public class ClientsDbContext : DbContext
    {
        public ClientsDbContext(DbContextOptions<ClientsDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Nom)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.Prenom)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.Username)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(c => c.Email)
                      .HasMaxLength(150);

                entity.Property(c => c.Query)
                      .HasMaxLength(500);

                entity.Property(c => c.CodePostal)
                      .IsRequired()
                      .HasMaxLength(10);

                entity.Property(c => c.Ville)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.Societe)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(c => c.CreatedAt)
                      .IsRequired();

                entity.Property(c => c.UpdatedAt);
            });
        }

    }

}
