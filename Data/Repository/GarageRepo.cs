﻿using Easypark_Backend.Data.DataModels;
using Easypark_Backend.Data.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
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
            _collection = mongoDb.GetCollection<GarageModel>("Garage");
        }

        public async Task UpdateGarageStatusAsync(string garageId, int status) 
        { 
           
            if (!ObjectId.TryParse(garageId, out var objectId))
            {
                throw new ArgumentException("Invalid garageeee ID format.", nameof(garageId));
            }
            var filter = Builders<GarageModel>.Filter.Eq(g => g.Id, garageId);
            var update = Builders<GarageModel>.Update.Set(g => g.Status, status);
            var result = await _collection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
            {
                throw new Exception($"Garage with ID {garageId} not found.");
            }
        }

        public async Task InsertGarageAsync(GarageModel garage)
        {
            if (garage == null)
            {
                throw new ArgumentNullException(nameof(garage), "Garage model cannot be null.");
            }

            await _collection.InsertOneAsync(garage);
        }
        public async Task<GarageModel> GetGarageByIdAsync(string garageId)
        {
            if (!ObjectId.TryParse(garageId, out var objectId))
            {
                throw new ArgumentException("Invalid garage ID format.", nameof(garageId));
            }

            var filter = Builders<GarageModel>.Filter.Eq(g => g.Id, garageId);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
