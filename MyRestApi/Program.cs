using System.Reflection;
using MyRestApi.Repositories;
using MyRestApi.Services;
using System.Data;
using Npgsql;
using Serilog;
using MyRestApi.Utils;
using Microsoft.EntityFrameworkCore;
using MyRestApi.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyRestApi.Models;

// NpgsqlConnection.GlobalTypeMapper.MapEnum<ReservationStatus>();

Log.Logger = new LoggerConfiguration()
    // .MinimumLevel.Warning() // 設定最低等級
    .MinimumLevel.Information() // 設定最低等級
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

if (builder.Environment.IsEnvironment("Test"))
{
    builder.Configuration.AddJsonFile("appsettings.Test.json");
}
else
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    DbMigration.EnsureDatabase(connectionString);
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
