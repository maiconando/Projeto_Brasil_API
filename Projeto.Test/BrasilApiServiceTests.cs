using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Projeto.Mappers.Interfaces;
using Projeto.Models.Weather;
using Projeto.Repository.Interfaces;
using Projeto.Services.Implementations;
using Projeto.Services.Interfaces;
using SDKBrasilAPI;
using SDKBrasilAPI.Responses;
using Xunit;

namespace Projeto.Test
{
    public class BrasilApiServiceTests
    {
        [Fact]
        public async Task GetByCityNameAsync_WithValidCityName_ReturnsWeatherResponse()
        {
            // Arrange
            var brasilApiMock = new Mock<IBrasilAPI>();
            var mapperMock = new Mock<IMapper>();
            var weatherRepositoryMock = new Mock<IWeatherRepository>();
            var logger = new LoggerFactory().CreateLogger<BrasilApiService>();

            var cityName = "Recife";
            var cityId = 123; 
            var brasilApiService = new BrasilApiService(
                brasilApiMock.Object, mapperMock.Object, weatherRepositoryMock.Object, logger
            );

            var citiesResponse = new CptecCidadeResponse
            {
                Cidades = new List<CptecCidade>
        {
            new CptecCidade { id = cityId, nome = cityName }
        }
            };

            var weatherBrasilApi = new CptecPrevisaoResponse
            {
                Estado = "PE",
                Cidade = "Pernambuco",
                AtualizadoEm = DateTimeOffset.Now.ToString(),

            };

            brasilApiMock.Setup(api => api.CptecCidade(It.IsAny<string>())).ReturnsAsync(citiesResponse);


            brasilApiMock.Setup(api => api.CptecClimaPrevisao(cityId)).ReturnsAsync(weatherBrasilApi);


            mapperMock.Setup(mapper => mapper.Map(It.IsAny<CptecPrevisaoResponse>())).Returns(new WeatherCityResponse
            {
                Id = cityId,
                City = cityName,
                State = "PE",
                UpdateOn = DateTimeOffset.Now,
            });

            // Act
            var result = await brasilApiService.GetByCityNameAsync(cityName);

            // Assert
            Assert.NotNull(result);

        }


        [Fact]
        public async Task GetBAirportCodeAsync_WithValidAirportCode_ReturnsWeatherResponse()
        {
            // Arrange
            var brasilApiMock = new Mock<IBrasilAPI>();
            var mapperMock = new Mock<IMapper>();
            var weatherRepositoryMock = new Mock<IWeatherRepository>();
            var loggerMock = new Mock<ILogger<BrasilApiService>>();

            var airportCode = "SBGL";
            var brasilApiService = new BrasilApiService(
                brasilApiMock.Object, mapperMock.Object, weatherRepositoryMock.Object, loggerMock.Object
            );

            brasilApiMock.Setup(api => api.CptecClimaAeroporto(airportCode)).ReturnsAsync(new CptecClimaResponse
            {
                Climas = new List<CptecClima>
        {
            new CptecClima
            {
                Condicao = "Teste",
                Temp = 10,
                CondicaoDesc = "CondicaoDesc",
                AtualizadoEm = DateTime.Now,
                CodigoIcao = "CodigoIcao",
                DirecaoVento = 2,
                Intensidade = "Intensidade",
                PressaoAtmosferica = 9,
                Umidade = 20,
                Vento = 50
            }
        }
            });

            mapperMock.Setup(mapper => mapper.Map(It.IsAny<CptecClima>())).Returns(new WeatherAirportResponse
            {
                Id = 1,
                Temperature = 30,
                AtmosphericPressure = 10,
                ConditionCode = "ps",
                ICAOCode = "10",
                ConditionDescription = "Predomínio de Sol",
                Moisture = 20,
                Visibility = "Visibility",
                WindDirection = 110,
                Wind = 14,
                UpdateOn = DateTimeOffset.Now

            });

            // Act
            var result = await brasilApiService.GetBAirportCodeAsync(airportCode);

            // Assert
            Assert.NotNull(result);

        }
    }

}
