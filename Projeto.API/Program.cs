using Projeto.CrossCutting.IoC;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Projeto.Services.Implementations;
using Projeto.Services.Interfaces;
using SDKBrasilAPI;
using System;
using Projeto.Infrastructure.Context;
using Projeto.Mappers.Interfaces;
using Projeto.Mappers.Implamentations;
using Microsoft.EntityFrameworkCore;
using Projeto.Repository.Implementations;
using Projeto.Repository.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBrasilApi();
builder.Services.AddScoped<IWeatherService, BrasilApiService>();
builder.Services.AddScoped<IMapper, Mapper>();
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddInfrastructureAPI(builder.Configuration);

var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext");
builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
         options.UseSqlServer(connectionString));
var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("startin application");


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
