using Easypark_Backend.Data.DataModels;
using Easypark_Backend.Data.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Easypark_Backend.Data.Repository
{
    public class GarageRepo
    {
        private readonly IMongoCollection<GarageModel> _collection;

        public GarageRepo(IOptions<EasyParkDBSetting> setting)
        {
            var mongoClient = new MongoClient(setting.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(setting.Value.DatabaseName);
            _collection = mongoDb.GetCollection<GarageModel>("Garages");
        }

        public async Task UpdateGarageStatusAsync(string garageId, int status)
        {
            var filter = Builders<GarageModel>.Filter.Eq(g => g.Id, garageId);
            var update = Builders<GarageModel>.Update.Set(g => g.Status, status);
            var result = await _collection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
            {
                throw new Exception($"Garage with ID {garageId} not found.");
            }
        }
    }
}
