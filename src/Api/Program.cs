using Infrastructure.Services.Auth.Adapters;
using Infrastructure.Services.Auth.PostgreSQL;
using Infrastructure.Services.Auth.SqlServer;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the database context for SQL Server (or Postgrees or MongoDb) on Client API - Not here
// builder.Services.AddIdentity<IdentityAppUser, IdentityRole>()
//     .AddEntityFrameworkStores<SqlServerAuthDbContext>() // Or PostgresAuthDbContext
//     .AddDefaultTokenProviders();

// Configure the database context for MongoDB
// Install AspNetCore.Identity.Mongo NuGet package if you want to use MongoDB
// builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//     .AddMongoDbStores<IdentityUser, IdentityRole, string>(
//         config["MongoDbSettings:ConnectionString"],
//         config["MongoDbSettings:DatabaseName"])
//     .AddDefaultTokenProviders();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
return;
