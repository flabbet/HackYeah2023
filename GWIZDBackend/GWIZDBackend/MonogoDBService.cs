using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GWIZDBackend;

public class MongoDBService
{
    private readonly IMongoCollection<AnimalReport> _reportCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _reportCollection = database.GetCollection<AnimalReport>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<List<AnimalReport>> GetAsync()
    {
        return await _reportCollection.Find(new BsonDocument()).ToListAsync();
    }

    [HttpPut("{id}")]
    public async Task CreateReportAsync(AnimalReport report)
    {
        await _reportCollection.InsertOneAsync(report);
    }

    public async Task DeleteAsync(string id)
    {

    }
}