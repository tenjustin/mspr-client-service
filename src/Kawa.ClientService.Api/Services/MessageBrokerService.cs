using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Kawa.ClientService.Api.Services;

/// <summary>
/// Interface pour la publication de messages via RabbitMQ
/// </summary>
public interface IMessageBrokerService
{
    void PublishMessage<T>(string exchange, string routingKey, T message);
}

/// <summary>
/// Service gérant la communication avec RabbitMQ
/// </summary>
public class MessageBrokerService : IMessageBrokerService, IDisposable
{
    private readonly IConnection _connection;     // Connexion RabbitMQ
    private readonly IModel _channel;             // Canal RabbitMQ
    private readonly ILogger<MessageBrokerService> _logger;
    private bool _disposed;

    public MessageBrokerService(IConnectionFactory connectionFactory, ILogger<MessageBrokerService> logger)
    {
        _logger = logger;
        try
        {
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _logger.LogInformation("Connexion à RabbitMQ établie avec succès");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Échec de la connexion à RabbitMQ");
            throw;
        }
    }

    public void PublishMessage<T>(string exchange, string routingKey, T message)
    {
        try
        {
            // Déclare l'échange de type Topic (permet le routage basé sur des patterns)
            _channel.ExchangeDeclare(exchange, ExchangeType.Topic, durable: true);
            
            var jsonMessage = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;  // Messages persistants
            properties.ContentType = "application/json";

            _channel.BasicPublish(
                exchange: exchange,
                routingKey: routingKey,
                basicProperties: properties,
                body: body);

            _logger.LogInformation("Message publié sur l'échange {Exchange} avec la clé de routage {RoutingKey}", 
                exchange, routingKey);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Échec de la publication du message sur l'échange {Exchange} avec la clé de routage {RoutingKey}", 
                exchange, routingKey);
            throw;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _channel?.Close();
            _channel?.Dispose();
            _connection?.Close();
            _connection?.Dispose();
        }

        _disposed = true;
    }
}