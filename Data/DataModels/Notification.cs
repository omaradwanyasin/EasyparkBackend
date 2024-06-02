using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class Notification
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("userId")]
    public string UserId { get; set; }

    [BsonElement("reservationId")]
    public string ReservationId { get; set; }

    [BsonElement("message")]
    public string Message { get; set; }

    [BsonElement("status")]
    public string Status { get; set; } = "unread";

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
