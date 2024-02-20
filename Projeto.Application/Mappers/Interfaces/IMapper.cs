using SDKBrasilAPI.Responses;
using Projeto.Models.Weather;

namespace Projeto.Mappers.Interfaces;
public interface IMapper
{
    WeatherCityResponse Map(CptecPrevisaoResponse cptecPrevisaoResponse);
    WeatherAirportResponse Map(CptecClima cptecClimaResponse);
}

