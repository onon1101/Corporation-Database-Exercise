
using System.Data;
using Npgsql;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using MyRestApi.Utils;
using Utils;
using System.Reflection;
using Microsoft.OpenApi.Models;
using MyRestApi.WebApi.Services;

// initial logger
Utils.Logger.Init();

// create web service.
var builder = WebApplication.CreateBuilder(args);

// settings
builder.Host.UseSerilog();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(SwaggerGenerator.Init);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthentication();

// extension from MyRestApi Module.
// builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddSingleton<KafkaProducer>();

// builder.ConfigureEnvironmentAndDatabase(); // if the env is test, then ...

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
