using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Easypark_Backend.Data.MongoDB;
using Easypark_Backend.Services;
using Easypark_Backend.Services.Hubs;
using Easypark_Backend.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Inspect the configuration to check if appsettings.json is loaded properly
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.Configure<EasyParkDBSetting>(configuration.GetSection("EasyParkDBSetting"));
builder.Services.AddSingleton<GarageServices>();
builder.Services.AddSingleton<UserLoggerRepo>();
builder.Services.AddControllers();

// Configure CORS to allow requests from http://localhost:3000
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();

// Apply CORS policy to allow requests from http://localhost:3000
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();
app.MapHub<ResearvationHub>("/Researve");

app.Run();






//var mongo = MongoDBConnection.Connection();
//IMongoDatabase database = mongo.GetDatabase("EasyParkDB");
//var collection = database.GetCollection<BsonDocument>("test");
//BsonDocument postDocument = new BsonDocument
//            {
//                { "content", "samer smara" },
//            };
//collection.InsertOne(postDocument);
