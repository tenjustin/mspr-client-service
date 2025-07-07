using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawa.ClientService.ServiceDefaults.Database.Models
{
    public class Client
    {
        public string Id { get; set; } = default!;
        public string Nom { get; set; } = default!;
        public string Prenom { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string? Email { get; set; }
        public string? Query { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Adresse
        public string CodePostal { get; set; } = default!;
        public string Ville { get; set; } = default!;

        // Société
        public string Societe { get; set; } = default!;
    }

}
