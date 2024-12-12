using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlazorCalendar.Domain;

public abstract class EntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; init; }
}