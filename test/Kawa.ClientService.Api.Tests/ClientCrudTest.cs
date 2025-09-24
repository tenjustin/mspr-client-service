using System.Net.Http.Json;
using AwesomeAssertions;
using Kawa.ClientService.ServiceDefaults.Database.Models;

namespace Kawa.ClientService.Api.Tests;

public class ClientCrudTest : IClassFixture<CustomFixture>
{
    private readonly CustomFixture _fixture;

    public ClientCrudTest(CustomFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetClient_ShouldReturn200()
    {
        // Arrange
        _fixture.Seeder.AddTestClients();
        var client = _fixture.Client;

        // Act
        var response = await client.GetAsync("api/client");
        var clients = await response.Content.ReadFromJsonAsync<List<Client>>();

        // Assert
        response.EnsureSuccessStatusCode();
        clients.Should().NotBeNullOrEmpty();
    }
}