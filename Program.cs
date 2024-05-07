//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//var app = builder.Build();

//// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();



//app.Run();

using Easypark_Backend.Data.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

var mongo = MongoDBConnection.Connection();
IMongoDatabase database = mongo.GetDatabase("EasyParkDB");
var collection = database.GetCollection<BsonDocument>("test");
BsonDocument postDocument = new BsonDocument
            {
                { "content", "testtest omar" },
            };
collection.InsertOne(postDocument);
