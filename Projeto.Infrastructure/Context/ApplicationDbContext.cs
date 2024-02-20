using Projeto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Projeto.Models.Weather;

namespace Projeto.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<WeatherAirportResponse> WeatherAirportResponse { get; set; }
        //public DbSet<ErrorMessage> Error { get; set; }
        public DbSet<WeatherCityResponse> WeatherCityResponse { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext)
                .Assembly);
        }
    }
}
