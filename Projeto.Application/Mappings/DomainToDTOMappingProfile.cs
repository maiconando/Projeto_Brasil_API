using AutoMapper;
using Projeto.Application.DTOs;
using Projeto.Domain.Entities;
using Projeto.Models.Weather;
using SDKBrasilAPI.Responses;

namespace Projeto.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<CptecPrevisaoResponse, WeatherCityResponse>().ReverseMap();
            CreateMap<WeatherAirportResponse, CptecClima>().ReverseMap();
        }
    }
}
