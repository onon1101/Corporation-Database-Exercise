
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
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Utils;
using System.Reflection;
using Confluent.Kafka;

// initial logger
Utils.Logger.Init();

// create web service.
var builder = WebApplication.CreateBuilder(args);

// settings
builder.Host.UseSerilog();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(SwaggerGenerator.Init);
builder.Services.AddJwtAuthentication(builder.Configuration); // jwt initialize.

// extension from MyRestApi Module.
builder.Services.AddAppServices(builder.Configuration);

builder.ConfigureEnvironmentAndDatabase(); // if the env is test, then ...

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<NotFoundHandlerMiddleware>();

app.MapControllers();

app.Run();

public partial class Program { }
