using System.Threading.Tasks;
using Projeto.Infrastructure.Context;
using Projeto.Repository.Interfaces;

namespace Projeto.Repository.Implementations;
public class WeatherRepository : IWeatherRepository
{
    private readonly ApplicationDbContext _context;
    public WeatherRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task SaveAsync<T>(T entity) where T : class
    {
         _context.Add(entity);
        await _context.SaveChangesAsync();
    }
}
