using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Json;
using Xunit;
using MyRestApi.Tests;
using MyRestApi.DTO;
using Microsoft.Identity.Client;
using MyRestApi.Models;

namespace MyRestApi.Tests;

public class UnitTest03_theater : IClassFixture<WebApplicationFactory<Program>>
{
    private HttpClient _client;

    private dynamic payload = new
    {
        name = "theater",
        location = "here",
        totalSeats = 1200
    };

    public UnitTest03_theater(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Test"); // 設定為 Test 環境
        }).CreateClient();
    }

    [Fact]
    public async Task CreateTheater_ReturnSuccessMessage()
    {
        await UnitTest_Util.GetResponse<TheaterResponse>(_client, "/api/theater/create", payload);
    }

    [Fact]
    public async Task GetAllTheater_ReturnSuccessMessage()
    {
        await UnitTest_Util.GetResponse<TheaterResponse>(_client, "/api/theater/create", payload);

        var response = await UnitTest_Util.GetResponse<IEnumerable<Theater>>(_client, "/api/theater/all");
        Assert.NotNull(response);
        Assert.Single(response);
    }

    [Fact]
    public async Task GetTheaterById_ReturnSuccessMessage()
    {
        var response_id = await UnitTest_Util.GetResponse<TheaterResponse>(_client, "/api/theater/create", payload);

        Console.WriteLine($"Database migration successful! {response_id.Id}");
        // throw new Exception(response_id);
        // var response = await UnitTest_Util.GetResponse<Theater>(_client, $"/api/theater/get/{response_id.result}");

        // Assert.Equal(response.Name, payload.name);
        // Assert.Equal(response.Location, payload.location);
        // Assert.Equal(response.TotalSeats, payload.total_seats);
    }
}

public class TheaterResponse
{
    public Guid Id { get; set; }
}

class TheaterGetAllResponse
{
    public List<Theater> result;
}