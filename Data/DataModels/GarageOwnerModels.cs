using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Easypark_Backend.Data.DataModels
{
    public class GarageOwnerModels
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } // MongoDB ID

        [BsonElement("GarageOwnerId")]
        public int? GarageOwnerId { get; set; } // Global ID

        [BsonElement("PhoneNumber")]
        public int? PhoneNumber { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Geometry")]
        public double[] Geometry { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
