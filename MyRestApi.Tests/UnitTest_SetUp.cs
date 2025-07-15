using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyRestApi.Tests;

public class TestSetup : IAsyncLifetime
{
    private readonly WebApplicationFactory<Program> _factory;
    public WebApplicationFactory<Program> Factory => _factory;

    public TestSetup()
    {
        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Test");
        });
    }

    public async Task InitializeAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var connectionString = config.GetConnectionString("DefaultConnection");

        MyRestApi.Utils.DbMigration.EnsureDatabase(connectionString);

        Console.WriteLine("✅ Test DB is ready!");
    }

    public Task DisposeAsync() => Task.CompletedTask;
}