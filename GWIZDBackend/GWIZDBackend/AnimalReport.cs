using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GWIZDBackend;

public class AnimalReport
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string AnimalId { get; set; } = null!;

    public string? PhotoBase64 { get; set; }

    public DateTime UploadTime { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }
}