using CuttSharp.Configurations;
using CuttSharp.Services;
using CuttSharp.Services.Telegram;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<CuttlyConfiguration>(builder.Configuration.GetSection("Cuttly"));

builder.Services.AddScoped<TelegramService>();

builder.Services.AddSingleton<IBotService, BotService>();

builder.Services.Configure<TelegramConfiguration>(builder.Configuration.GetSection("Telegram"));

builder.Services.AddHostedService<TelegramService>();

builder.Services.AddHttpClient<CuttlyService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
