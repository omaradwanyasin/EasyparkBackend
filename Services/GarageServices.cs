using Easypark_Backend.Data.DataModels;
using Easypark_Backend.Data.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace Easypark_Backend.Services
{
    public class GarageServices
    {
        private readonly IMongoCollection<GarageModel> _GarageCollection;
        public GarageServices(IOptions<EasyParkDBSetting> setting) {
            var mongoClient = new MongoClient(setting.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(setting.Value.DatabaseName);
            _GarageCollection = mongoDb.GetCollection<GarageModel>("Garages");
        }
        public async Task<List<GarageModel>> getAsyncAllGarages()
        {
            return await _GarageCollection.Find( _ => true).ToListAsync();
        }

    }
}
