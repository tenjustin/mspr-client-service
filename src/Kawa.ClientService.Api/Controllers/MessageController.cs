using Kawa.ClientService.Api.Models;
using Kawa.ClientService.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kawa.ClientService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageBrokerService _messageBroker;
    private readonly ILogger<MessageController> _logger;

    public MessageController(IMessageBrokerService messageBroker, ILogger<MessageController> logger)
    {
        _messageBroker = messageBroker;
        _logger = logger;
    }

    [HttpPost]
    public IActionResult SendMessage([FromBody] ClientMessage message)
    {
        try
        {
            // Configuration d'échange et de clé de routage
            const string exchange = "client-service";
            var routingKey = $"client.{message.Action?.ToLower() ?? "default"}";

            _messageBroker.PublishMessage(exchange, routingKey, message);

            return Ok(new { success = true, messageId = message.Id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'envoi du message");
            return StatusCode(500, "Une erreur s'est produite lors de l'envoi du message");
        }
    }
}