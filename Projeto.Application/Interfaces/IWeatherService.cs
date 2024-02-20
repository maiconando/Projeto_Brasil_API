using System.Threading.Tasks;
using Projeto.Models.Weather;

namespace Projeto.Services.Interfaces;
public interface IWeatherService
{
    Task<WeatherCityResponse> GetByCityNameAsync(string cityName);
    Task<WeatherAirportResponse> GetByAirportCodeAsync(string airportCode);
}
