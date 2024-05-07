using MongoDB.Driver;

namespace Easypark_Backend.Data.MongoDB
{
    public class MongoDBConnection
    {
        private readonly IMongoClient _client; //interface representing a client to MongoDB
        private static MongoDBConnection _connection = null;

        private MongoDBConnection() // connect with MongoDB
        {
            string connectionString = "mongodb+srv://omaryasin:AUuQzZ0gG6G4gUvT@cluster0.5rpwsoi.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
            _client = new MongoClient(connectionString);
        }
        public static MongoDBConnection Connection()
        {
            if (_connection == null)
                return _connection = new MongoDBConnection();

            return _connection;
        }


        public IMongoDatabase GetDatabase(string databaseName)
        {
            return _client.GetDatabase(databaseName);
        }
    }
}
