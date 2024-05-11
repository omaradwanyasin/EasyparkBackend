using Easypark_Backend.Business.Dtos;
using Easypark_Backend.Data.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Easypark_Backend.Data.Repository
{
    public class GarageRepo
    {
        private IMongoCollection<BsonDocument> _collection;

        public GarageRepo()
        {
            var mongo = MongoDBConnection.Connection();
            IMongoDatabase database = mongo.GetDatabase("EasyParkDB");
            _collection = database.GetCollection<BsonDocument>("test");
        }

        public List<BsonDocument> GetAllGarages()
        {
            var documents = _collection.Find(new BsonDocument()).ToList();
            return documents;
        }
    }
}
