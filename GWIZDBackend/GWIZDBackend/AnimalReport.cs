using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GWIZDBackend;

public class AnimalReport
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string animalId { get; set; } = null!;

    public ObjectId? photoId { get; set; }

    public DateTime UploadTime { get; set; }
}