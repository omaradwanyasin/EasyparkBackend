using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Easypark_Backend.Data.MongoDB;
using Easypark_Backend.Data.Repository;
using signalrtest.Hubs;
using Easypark_Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Easypark_Backend;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Inspect the configuration to check if appsettings.json is loaded properly
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.Configure<EasyParkDBSetting>(configuration.GetSection("EasyParkDBSetting"));
builder.Services.AddSingleton<GarageRepo>();
builder.Services.AddSingleton<GarageServices>();
builder.Services.AddSingleton<UserLoggerRepo>();
builder.Services.AddSingleton<NotificationsRepo>();
builder.Services.AddSingleton<NotificationHub>();
//builder.Services.AddSingleton(JwtOptions);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "EasyPark", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
               .SetIsOriginAllowed(origin => true); // Allow any origin
    });
});
var jwtoptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
builder.Services.AddSingleton(jwtoptions);
builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.SaveToken = true; //
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtoptions.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtoptions.Audience,
            ValidateSignatureLast = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions.SigningKey))
        };
    });
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EasyPark v1");
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseCors("CorsPolicy");
app.MapHub<GarageHubs>("/garageHubs");
app.MapHub<NotificationHub>("/notificationHub");


app.Run();
