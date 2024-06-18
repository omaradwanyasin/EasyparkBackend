using Easypark_Backend.Data.DataModels;
using Easypark_Backend.Data.MongoDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easypark_Backend.Data.Repository
{
    public class ReservationRepo
    {
        private readonly IMongoCollection<ReservationModel> _collection;

        public ReservationRepo(IOptions<EasyParkDBSetting> setting)
        {
            var mongoClient = new MongoClient(setting.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(setting.Value.DatabaseName);
            _collection = mongoDb.GetCollection<ReservationModel>("Reservartion");
        }

        public async Task<List<ReservationModel>> getReservations()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<ActionResult> addreservation(ReservationModel ob)
        {
            if (ob == null)
            {
                return new BadRequestObjectResult("Reservation data is null");
            }

            await _collection.InsertOneAsync(ob);
            
            return new CreatedAtActionResult("GetReservationById", "Reservation", new { id = ob.Id }, ob);
        }

        public async Task<List<ReservationModel>> GetReservationByIdAsync(string garageId)
        {
            return await _collection.Find(r => r.GarageId == garageId).ToListAsync();
        }



        public async Task<bool> deleteRes(string reservationid)
        {
            var deleteResult = await _collection.DeleteOneAsync(r => r.Id == reservationid);

            return deleteResult.DeletedCount > 0;
        }

    }
}
