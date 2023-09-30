using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace GWIZDBackend;

public class MongoDBService
{
    private readonly IMongoCollection<AnimalReport> _reportCollection;
    private IGridFSBucket _bucket;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _reportCollection = database.GetCollection<AnimalReport>(mongoDBSettings.Value.CollectionName);
        _bucket = new GridFSBucket(database);
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

    public async Task<ObjectId> UploadPhotoAsync(IFormFile photo)
    {
        ObjectId id = ObjectId.GenerateNewId();
        await _bucket.UploadFromStreamAsync(id.ToString(), photo.OpenReadStream());
        return id;
    }

    public async Task DeleteAsync(string id)
    {

    }
}