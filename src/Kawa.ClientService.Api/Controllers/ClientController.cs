using Kawa.ClientService.Api.Services;
using Kawa.ClientService.ServiceDefaults.Database;
using Kawa.ClientService.ServiceDefaults.Database.Models;
using Kawa.ClientService.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kawa.ClientService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientsDbContext _context;
        private readonly IMessageBrokerService _messageBrokerService;

        public ClientController(ClientsDbContext context, IMessageBrokerService messageBrokerService)
        {
            _context = context;
            _messageBrokerService = messageBrokerService;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            return Ok(_context.Clients.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Client client)
        {
            if (client == null)
            {
                return BadRequest();
            }

            _context.Clients.Add(client);
            _context.SaveChanges();

            // Envoyer un message de création de client
            var message = new ClientMessage
            {
                Action = "ClientCreated",
                Content = $"Client with ID {client.Id} has been created."
            };
            _messageBrokerService.PublishMessage("kawa-exchange", "kawa-client", message);

            return CreatedAtAction(nameof(GetCustomerById), new { id = client.Id }, client);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Client updatedClient)
        {
            if (updatedClient == null || updatedClient.Id != id.ToString())
            {
                return BadRequest();
            }

            var existingClient = _context.Clients.Find(id);
            if (existingClient == null)
            {
                return NotFound();
            }

            existingClient.Nom = updatedClient.Nom;
            existingClient.Email = updatedClient.Email;
            existingClient.Prenom = updatedClient.Prenom;
            existingClient.Username = updatedClient.Username;
            existingClient.Query = updatedClient.Query;
            existingClient.UpdatedAt = DateTime.UtcNow;
            existingClient.CodePostal = updatedClient.CodePostal;
            existingClient.Ville = updatedClient.Ville;
            existingClient.Societe = updatedClient.Societe;

            _context.Clients.Update(existingClient);
            _context.SaveChanges();

            // Envoyer un message de mise à jour de client
            var message = new ClientMessage
            {
                Action = "ClientUpdated",
                Content = $"Client with ID {existingClient.Id} has been updated."
            };
            _messageBrokerService.PublishMessage("kawa-exchange", "kawa-client", message);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            _context.SaveChanges();

            // Envoyer un message de suppression de client
            var message = new ClientMessage
            {
                Action = "ClientDeleted",
                Content = $"Client with ID {client.Id} has been deleted."
            };
            _messageBrokerService.PublishMessage("kawa-exchange", "kawa-client", message);

            return NoContent();
        }
    }
}
