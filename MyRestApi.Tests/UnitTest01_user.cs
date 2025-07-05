using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Xunit;

namespace MyRestApi.Tests;

public class RegisterUserTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public RegisterUserTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Test"); // 設定為 Test 環境
        }).CreateClient();
    }

    [Fact]
    public async Task CanConnectToDatabase()
    {
        var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=postgres_test";

        await using var conn = new Npgsql.NpgsqlConnection(connectionString);
        try
        {
            await conn.OpenAsync();
            Assert.True(conn.State == System.Data.ConnectionState.Open);
        }
        catch (Exception ex)
        {
            Assert.False(true, $"Failed to connect: {ex.Message}");
        }
    }

    // [Fact]
    public async Task RegisterUser_ReturnsSuccessMessage()
    {
        var payload = new
        {
            username = "testuser",
            email = "test@example.com",
            password = "test123",
        };

        var response = await _client.PostAsJsonAsync("/api/User/Register", payload);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<RegisterResponse>();
        Assert.Equal("User 'testuser' registered successfully.", result!.Message);
    }

    [Fact]
    public async Task DeleteUser_ReturnSuccessMessage()
    {
        var payload = new
        {
            username = "testuser",
            email = "test@example.com",
            password = "test123",
        };

        var response = await _client.PostAsJsonAsync("/api/User/Register", payload);
        response.EnsureSuccessStatusCode();
    }

    // [Fact]
    public async Task ModifyUser_ReturnSuccessMessage()
    {
        var payload = new
        {
            username = "tatuser",
            email = "tatuser",
            password = "t",
        };

        var response = await _client.PostAsJsonAsync("/api/registeruser", payload);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<RegisterResponse>();
        Assert.Equal("successfully", result!.Message);
    }

    public class RegisterResponse
    {
        public string Message { get; set; } = "";
    }
}
