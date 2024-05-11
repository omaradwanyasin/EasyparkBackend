using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Easypark_Backend.Data.DataModels
{
    public class GarageModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("geometry")]
        public double[] Geometry { get; set; }

        [BsonElement("properties")]
        public Dictionary<string, object> Properties { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("comments")]
        public List<string> Comments { get; set; }

        [BsonElement("City")]
        public string City { get; set; }

        [BsonElement("status")]
        public int Status { get; set; }

        [BsonElement("info")]
        public string Info { get; set; }

        [BsonElement("rating")]
        public int Rating { get; set; }
    }
}
