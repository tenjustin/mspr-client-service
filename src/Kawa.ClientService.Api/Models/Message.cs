namespace Kawa.ClientService.Api.Models;

public class ClientMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Action { get; set; }
    public string? Content { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}