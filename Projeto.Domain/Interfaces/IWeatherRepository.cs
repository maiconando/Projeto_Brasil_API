using System.Threading.Tasks;
using Projeto.Models.Weather;

namespace Projeto.Repository.Interfaces;
public interface IWeatherRepository
{
    Task SaveAsync<T>(T entity) where T: class;
}

