using Projeto.Application.Mappings;
using Projeto.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Projeto.Services.Implementations;
using Projeto.Services.Interfaces;
using Projeto.Repository.Implementations;
using Projeto.Repository.Interfaces;

namespace Projeto.CrossCutting.IoC;

public static class DependencyInjectionAPI
{
    public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
           options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                 new MySqlServerVersion(new Version(8, 0, 11))));

        services.AddScoped<IWeatherRepository, WeatherRepository >();
        services.AddScoped<IWeatherService, BrasilApiService>();

        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

        return services;
    }
}
