using System.Reflection;
using MyRestApi.Repositories;
using MyRestApi.Services;
using System.Data;
using Npgsql;
using MyRestApi.Utils;
using Microsoft.EntityFrameworkCore;
using MyRestApi.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyRestApi.Models;

// NpgsqlConnection.GlobalTypeMapper.MapEnum<ReservationStatus>();

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsEnvironment("Test"))
{
    builder.Configuration.AddJsonFile("appsettings.Test.json");
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// extension from MyRestApi Module.
builder.Services.AddAppServices(builder.Configuration);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
DbMigration.EnsureDatabase(connectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// 掛載 Controller
app.MapControllers();

app.Run();

public partial class Program { }