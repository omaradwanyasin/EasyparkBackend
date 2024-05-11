using Easypark_Backend.Data.DataModels;
using Easypark_Backend.Data.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Easypark_Backend.Data.Repository
{
    public class UserLoggerRepo
    {
        private readonly IMongoCollection<UserModels> _UserCollection;
        public UserLoggerRepo(IOptions<EasyParkDBSetting> setting)
        {
            var mongoClient = new MongoClient(setting.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(setting.Value.DatabaseName);
            _UserCollection = mongoDb.GetCollection<UserModels>("User");
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


    }
}
