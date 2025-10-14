using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using viesbuciu_rezervacija_backend.Data;
using viesbuciu_rezervacija_backend.Interfaces;
using viesbuciu_rezervacija_backend.Repository;
DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var dbServer = Environment.GetEnvironmentVariable("DB_SERVER");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var connectionString = $"Server={dbServer},1433;Initial Catalog={dbName};Persist Security Info=False;User ID={dbUser};Password={dbPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<HotelDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

