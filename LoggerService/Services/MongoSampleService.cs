using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

public class MongoSampleService : BackgroundService
{
    private readonly IMongoDatabase _database;
    private readonly ILogger<MongoSampleService> _logger;

    public MongoSampleService(IMongoDatabase database, ILogger<MongoSampleService> logger)
    {
        _database = database;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var collection = _database.GetCollection<dynamic>("TestCollection");

        _logger.LogInformation("🟢 Connected to MongoDB. Inserting test document...");

        await collection.InsertOneAsync(new { name = "test", time = DateTime.UtcNow }, cancellationToken: stoppingToken);

        _logger.LogInformation("✅ Document inserted. Shutting down...");
    }
}
