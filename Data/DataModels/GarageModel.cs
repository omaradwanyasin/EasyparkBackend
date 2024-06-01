using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Easypark_Backend.Data.DataModels
{
    public class GarageModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("geometry")]
        public List<double> Geometry { get; set; }

        [BsonElement("properties")]
        public PropertyModel Properties { get; set; }

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

        [BsonElement("capacity")]
        public int Capacity { get; set; }

        [BsonElement("containsWifi")]
        public bool ContainsWifi { get; set; }

        [BsonElement("supportsElectricalCharging")]
        public bool SupportsElectricalCharging { get; set; }

        [BsonElement("supportsHeavyTrucks")]
        public bool SupportsHeavyTrucks { get; set; }

        [BsonElement("garageid")]
        public string GarageId { get; set; }
    }

    public class PropertyModel
    {
        [BsonElement("prop0")]
        public string Prop0 { get; set; }

        [BsonElement("parkid")]
        public int ParkId { get; set; }
    }
}
