using Easypark_Backend.Data.DataModels;
using Easypark_Backend.Data.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Easypark_Backend.Data.Repository
{
    public class GarageRepo
    {
        private readonly IMongoCollection<GarageModel> _collection;
        private readonly ILogger<GarageRepo> _logger;

        public GarageRepo(IOptions<EasyParkDBSetting> setting, ILogger<GarageRepo> logger)
        {
            var mongoClient = new MongoClient(setting.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(setting.Value.DatabaseName);
            _collection = mongoDb.GetCollection<GarageModel>("Garage");
            _logger = logger;
        }

        public async Task UpdateGarageStatusAsync(string garageId, int status)
        {
            if (!ObjectId.TryParse(garageId, out var objectId))
            {
                _logger.LogError($"Invalid garage ID format: {garageId}");
                throw new ArgumentException("Invalid garage ID format.", nameof(garageId));
            }

            var filter = Builders<GarageModel>.Filter.Eq(g => g.Id, garageId);
            var update = Builders<GarageModel>.Update.Set(g => g.Status, status);
            var result = await _collection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
            {
                _logger.LogWarning($"Garage with ID {garageId} not found.");
                throw new Exception($"Garage with ID {garageId} not found.");
            }

            _logger.LogInformation($"Garage status updated: ID = {garageId}, Status = {status}");
        }

        public async Task InsertGarageAsync(GarageModel garage)
        {
            if (garage == null)
            {
                _logger.LogError("Attempted to insert a null garage model.");
                throw new ArgumentNullException(nameof(garage), "Garage model cannot be null.");
            }

            try
            {
                await _collection.InsertOneAsync(garage);
                _logger.LogInformation($"Inserted new garage: {garage.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting garage.");
                throw;
            }
        }

        public async Task<GarageModel> GetGarageByIdAsync(string garageId)
        {
            if (!ObjectId.TryParse(garageId, out var objectId))
            {
                _logger.LogError($"Invalid garage ID format: {garageId}");
                throw new ArgumentException("Invalid garage ID format.", nameof(garageId));
            }

            var filter = Builders<GarageModel>.Filter.Eq(g => g.Id, garageId);
            var garage = await _collection.Find(filter).FirstOrDefaultAsync();

            if (garage == null)
            {
                _logger.LogWarning($"Garage with ID {garageId} not found.");
            }

            return garage;
        }
    }
}
