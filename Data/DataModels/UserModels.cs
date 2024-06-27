using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Easypark_Backend.Data.DataModels
{
    public class UserModels
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("UserId")] bool 
        public int UserId { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }
    }
}
