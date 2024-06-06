using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class ReservationModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("user_id")]
    public string UserId { get; set; }

    [BsonElement("garage_id")]
    public string GarageId { get; set; }

    [BsonElement("reservation_date")]
    public DateTime ReservationDate { get; set; }

    [BsonElement("is_confirmed")]
    public bool IsConfirmed { get; set; }
    [BsonElement("email")]
    public string Email { get; set; }
    [BsonElement("phoneNumber")]
    public string phone { get; set; }
}


