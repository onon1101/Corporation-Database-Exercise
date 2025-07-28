using LoggerService.Worker.Models;
using MongoDB.Driver;

namespace LoggerService.Repositories;

public class MongoBusRepository
{
    private readonly IMongoDatabase _database;

    public MongoBusRepository(IMongoDatabase database)
    {
        _database = database;
    }

    public async Task Storage(LogEntry log)
    {
       var collection = _database.GetCollection<LogEntry>("TestCollection");
       await collection.InsertOneAsync(log);
    }
}