using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SDKBrasilAPI;
using Projeto.Models.Weather;
using Projeto.Repository.Interfaces;
using Projeto.Services.Interfaces;
using Projeto.Mappers.Interfaces;

namespace Projeto.Services.Implementations
{
    public class BrasilApiService : IWeatherService
    {
        private readonly IBrasilAPI _brasilAPI;
        private readonly IMapper _mapper;
        private readonly IWeatherRepository _weatherRepository;
        private readonly ILogger<BrasilApiService> _logger;

        public BrasilApiService(IBrasilAPI brasilAPI, IMapper mapper, IWeatherRepository weatherRepository, ILogger<BrasilApiService> logger)
        {
            _brasilAPI = brasilAPI ?? throw new ArgumentNullException(nameof(brasilAPI));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _weatherRepository = weatherRepository ?? throw new ArgumentNullException(nameof(weatherRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Obtém informações meteorológicas com base no nome da cidade.
        /// </summary>
        /// <param name="cityName">Nome da cidade.</param>
        /// <returns>Informações meteorológicas da cidade.</returns>
        public async Task<WeatherCityResponse> GetByCityNameAsync(string cityName)
        {
            try
            {
                // Exemplo https://brasilapi.com.br/api/cptec/v1/cidade/Recife" 
                var citiesResponse = await _brasilAPI.CptecCidade(cityName[..4]);
                var city = citiesResponse.Cidades.FirstOrDefault(c => c.nome == cityName);

                if (city == null)
                {
                    _logger.LogWarning("Cidade não encontrada: {CityName}", cityName);
                    return null;
                }

                var weatherBrasilApi = await _brasilAPI.CptecClimaPrevisao(city.id);
                var weatherResponse = _mapper.Map(weatherBrasilApi);
                await _weatherRepository.SaveAsync(weatherResponse);

                return weatherResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar buscar clima pelo nome");
                throw;
            }
        }

        /// <summary>
        /// Obtém informações meteorológicas com base no código do aeroporto.
        /// </summary>
        /// <param name="airportCode">Código do aeroporto.</param>
        /// <returns>Informações meteorológicas do aeroporto.</returns>
        public async Task<WeatherAirportResponse> GetByAirportCodeAsync(string airportCode)
        {
            try
            {
                // Exemplo https://brasilapi.com.br/api/cptec/v1/clima/aeroporto/SBGL"
                var weatherBrasilApi = (await _brasilAPI.CptecClimaAeroporto(airportCode)).Climas.FirstOrDefault();

                if (weatherBrasilApi == null)
                {
                    _logger.LogWarning("Clima não encontrado para o aeroporto: {AirportCode}", airportCode);
                    return null;
                }

                var weatherResponse = _mapper.Map(weatherBrasilApi);
                await _weatherRepository.SaveAsync(weatherResponse);

                return weatherResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar buscar clima pelo aeroporto");
                throw;
            }
        }
    }
}
