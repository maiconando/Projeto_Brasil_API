using System;
using System.Collections.Generic;
using SDKBrasilAPI.Responses;
using Projeto.Mappers.Interfaces;
using Projeto.Models.Weather;

namespace Projeto.Mappers.Implamentations;
public class Mapper : IMapper
{
    public WeatherCityResponse Map(CptecPrevisaoResponse cptecPrevisaoResponse)
    {
        var weatherDetails = new List<WeatherCity>();
        var weatherResponse = new WeatherCityResponse
        {
            City = cptecPrevisaoResponse.Cidade,
            State = cptecPrevisaoResponse.Estado,
            UpdateOn = DateTime.Parse(cptecPrevisaoResponse.AtualizadoEm),
        };
        foreach (var item in cptecPrevisaoResponse.Clima)
        {
            weatherDetails.Add(new WeatherCity
            {
                Date = DateTime.Parse(item.Data),
                ConditionCode = item.Condicao,
                ConditionDescription = item.CondicaoDesc,
                MinTemperature = item.Min,
                MaxTemperature = item.Max,
                UvIndex = item.IndiceUv
            });
        }
        weatherResponse.Weather = weatherDetails;
        return weatherResponse;

    }

    public WeatherAirportResponse Map(CptecClima cptecClima)
    {
        return new WeatherAirportResponse
        {
            Moisture = cptecClima.Umidade,
            Visibility = cptecClima.Intensidade,
            ICAOCode = cptecClima.CodigoIcao,
            AtmosphericPressure = cptecClima.PressaoAtmosferica,
            Wind = cptecClima.Vento,
            WindDirection = cptecClima.DirecaoVento,
            ConditionCode = cptecClima.Condicao,
            ConditionDescription = cptecClima.CondicaoDesc,
            Temperature = cptecClima.Temp,
            UpdateOn = cptecClima.AtualizadoEm.GetValueOrDefault()
        };
    }
}

