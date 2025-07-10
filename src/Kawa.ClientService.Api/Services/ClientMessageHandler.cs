using Kawa.ClientService.Api.Models;

namespace Kawa.ClientService.Api.Services;

/// <summary>
/// Implémentation du gestionnaire de messages clients
/// </summary>
public class ClientMessageHandler : IMessageHandler<ClientMessage>
{
    private readonly ILogger<ClientMessageHandler> _logger;

    public ClientMessageHandler(ILogger<ClientMessageHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleMessageAsync(ClientMessage message, string routingKey)
    {
        _logger.LogInformation("Traitement du message: ID={Id}, Action={Action}, Contenu={Content}",
            message.Id, message.Action, message.Content);

        // Implémentez votre logique métier ici
        // Par exemple:
        switch (message.Action?.ToLower())
        {
            case "create":
                _logger.LogInformation("Création d'un nouveau client");
                break;
            case "update":
                _logger.LogInformation("Mise à jour d'un client");
                break;
            case "delete":
                _logger.LogInformation("Suppression d'un client");
                break;
            default:
                _logger.LogWarning("Action inconnue: {Action}", message.Action);
                break;
        }

        return Task.CompletedTask;
    }
}