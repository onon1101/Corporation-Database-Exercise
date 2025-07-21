using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// 加入 ocelot 設定
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// 註冊 Ocelot
builder.Services.AddOcelot();

var app = builder.Build();

// 啟用 Ocelot Middleware
await app.UseOcelot();

app.Run();