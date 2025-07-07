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

        public DbSet<Client> Clients { get; set; } = default!;
    }

}
