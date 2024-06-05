using Easypark_Backend.Data.DataModels;
using Easypark_Backend.Data.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Easypark_Backend.Data.Repository
{
    public class UserLoggerRepo
    {
        private readonly IMongoCollection<UserModels> _UserCollection;
        private readonly IMongoCollection<GarageOwnerModels> _garageCollection;
        public UserLoggerRepo(IOptions<EasyParkDBSetting> setting)
        {
            var mongoClient = new MongoClient(setting.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(setting.Value.DatabaseName);
            _UserCollection = mongoDb.GetCollection<UserModels>("User");
            _garageCollection = mongoDb.GetCollection<GarageOwnerModels>("GarageOwner");
        }
        public async Task<UserModels> SignIn(string email, string password)
        {
            var filter = Builders<UserModels>.Filter.Eq(u => u.Email, email) &
                         Builders<UserModels>.Filter.Eq(u => u.Password, password);
            var user = await _UserCollection.Find(filter).FirstOrDefaultAsync();

            return user;
        }

        public async Task<UserModels> SignUp(UserModels newUser)
        {
            try
            {
                await _UserCollection.InsertOneAsync(newUser);
                return newUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred during user registration: {ex}");
                throw;
            }
        }

        public async Task<GarageOwnerModels> SignInGarageOwner(string email, string password)
        {
            var filter = Builders<GarageOwnerModels>.Filter.Eq(u => u.Email, email) &
                         Builders<GarageOwnerModels>.Filter.Eq(u => u.Password, password);
            var user = await _garageCollection.Find(filter).FirstOrDefaultAsync();

            return user;
        }
        ///here the sign up for the garage owner;

        public async Task<GarageOwnerModels> GarageOwnerSignUp(GarageOwnerModels newUser)
        {
            try
            {
                await _garageCollection.InsertOneAsync(newUser);
                return newUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred during user registration: {ex}");
                throw;
            }
        }


    }
}
