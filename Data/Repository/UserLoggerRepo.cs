using Easypark_Backend.Data.DataModels;
using Easypark_Backend.Data.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace Easypark_Backend.Data.Repository
{
    public class UserLoggerRepo
    {
        private readonly IMongoCollection<UserModels> _UserCollection;
        private readonly IMongoCollection<GarageOwnerModels> _garageCollection;
        private readonly ILogger<UserLoggerRepo> _logger;

        public UserLoggerRepo(IOptions<EasyParkDBSetting> setting, ILogger<UserLoggerRepo> logger)
        {
            var mongoClient = new MongoClient(setting.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(setting.Value.DatabaseName);
            _UserCollection = mongoDb.GetCollection<UserModels>("User");
            _garageCollection = mongoDb.GetCollection<GarageOwnerModels>("GarageOwner");
            _logger = logger;
        }

        public async Task<UserModels> SignIn(string email, string password)
        {
            try
            {
                var hashedPassword = HashPassword(password);
                var filter = Builders<UserModels>.Filter.Eq(u => u.Email, email) &
                             Builders<UserModels>.Filter.Eq(u => u.Password, hashedPassword);
                var user = await _UserCollection.Find(filter).FirstOrDefaultAsync();

                if (user == null)
                {
                    _logger.LogWarning($"Failed login attempt for email: {email}");
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user sign-in.");
                throw;
            }
        }

        public async Task<UserModels> SignUp(UserModels newUser)
        {
            try
            {
                var existingUser = await _UserCollection.Find(Builders<UserModels>.Filter.Eq(u => u.Email, newUser.Email)).FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    _logger.LogWarning($"Attempted to register with existing email: {newUser.Email}");
                    throw new InvalidOperationException("Email already in use.");
                }

                newUser.Password = HashPassword(newUser.Password);
                await _UserCollection.InsertOneAsync(newUser);
                _logger.LogInformation($"User registered successfully: {newUser.Email}");
                return newUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user registration.");
                throw;
            }
        }

        public async Task<GarageOwnerModels> SignInGarageOwner(string email, string password)
        {
            try
            {
                var hashedPassword = HashPassword(password);
                var filter = Builders<GarageOwnerModels>.Filter.Eq(u => u.Email, email) &
                             Builders<GarageOwnerModels>.Filter.Eq(u => u.Password, hashedPassword);
                var user = await _garageCollection.Find(filter).FirstOrDefaultAsync();

                if (user == null)
                {
                    _logger.LogWarning($"Failed login attempt for garage owner email: {email}");
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during garage owner sign-in.");
                throw;
            }
        }

        public async Task<GarageOwnerModels> GarageOwnerSignUp(GarageOwnerModels newUser)
        {
            try
            {
                var existingUser = await _garageCollection.Find(Builders<GarageOwnerModels>.Filter.Eq(u => u.Email, newUser.Email)).FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    _logger.LogWarning($"Attempted to register with existing garage owner email: {newUser.Email}");
                    throw new InvalidOperationException("Email already in use.");
                }

                newUser.Password = HashPassword(newUser.Password);
                await _garageCollection.InsertOneAsync(newUser);
                _logger.LogInformation($"Garage owner registered successfully: {newUser.Email}");
                return newUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during garage owner registration.");
                throw;
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
