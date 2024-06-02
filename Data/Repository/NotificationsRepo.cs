using Easypark_Backend.Data.MongoDB;

using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Easypark_Backend.Data.Repository
{
    public class NotificationsRepo
    {
        private readonly IMongoCollection<Notification> _notificationsCollection;

        public NotificationsRepo(IOptions<EasyParkDBSetting> easyParkDBSettings)
        {
            var mongoClient = new MongoClient(easyParkDBSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(easyParkDBSettings.Value.DatabaseName);

            _notificationsCollection = mongoDatabase.GetCollection<Notification>("Notification");
        }

        public async Task CreateAsync(Notification notification) =>
            await _notificationsCollection.InsertOneAsync(notification);

        public async Task<List<Notification>> GetUserNotificationsAsync(string userId) =>
            await _notificationsCollection.Find(n => n.UserId == userId).ToListAsync();
    }
}
