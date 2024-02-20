using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Adicionado o namespace para ILogger
using Projeto.Services.Interfaces;
using System.Threading.Tasks;

namespace Projeto.Controllers
{
    [Route("api/v1/[Controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly ILogger<WeatherController> _logger;

        private const string ERROR_MESSAGE_EMPTY_PARAMETER = "O parâmetro deve ser preenchido";
        private const string ERROR_MESSAGE_LENGTH_WRONG = "O parâmetro deve ter {0} caracteres";

        public WeatherController(IWeatherService weatherService, ILogger<WeatherController> logger)
        {
            _weatherService = weatherService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém informações meteorológicas com base no nome da cidade.
        /// </summary>
        /// <param name="cityName">Nome da cidade.</param>
        /// <returns>Informações meteorológicas da cidade.</returns>
        [HttpGet("GetByCityName/{cityName}")]
        public async Task<IActionResult> GetByCityNameAsync(string cityName)
        {
            _logger.LogTrace("Request {MethodName} - CityName: {CityName}", nameof(GetByCityNameAsync), cityName);

            if (string.IsNullOrEmpty(cityName))
            {
                return BadRequest(ERROR_MESSAGE_EMPTY_PARAMETER);
            }

            //https://brasilapi.com.br/api/cptec/v1/cidade/Recife
            var weather = await _weatherService.GetByCityNameAsync(cityName);
            _logger.LogTrace("Response {Weather}", weather);

            return Ok(weather);
        }

        /// <summary>
        /// Obtém informações meteorológicas com base no código do aeroporto.
        /// </summary>
        /// <param name="airportCode">Código do aeroporto.</param>
        /// <returns>Informações meteorológicas do aeroporto.</returns>
        [HttpGet("GetByAirportCode/{airportCode}")]
        public async Task<IActionResult> GetByAirportAsync(string airportCode)
        {
            _logger.LogTrace("Request {MethodName} - AirportCode: {AirportCode}", nameof(GetByAirportAsync), airportCode);

            if (string.IsNullOrEmpty(airportCode))
            {
                return BadRequest(ERROR_MESSAGE_EMPTY_PARAMETER);
            }

            if (airportCode.Length != 4)
            {
                return BadRequest(string.Format(ERROR_MESSAGE_LENGTH_WRONG, 4));
            }

            //https://brasilapi.com.br/api/cptec/v1/clima/aeroporto/SBGL
            var weather = await _weatherService.GetByAirportCodeAsync(airportCode);
            _logger.LogTrace("Response {Weather}", weather);

            return Ok(weather);
        }
    }
}
