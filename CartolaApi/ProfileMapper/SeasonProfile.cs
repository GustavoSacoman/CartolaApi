using AutoMapper;
using CartolaApi.Data.DTOs;
using CartolaApi.Routes.Models;

namespace CartolaApi.ProfileMapper;

public class SeasonProfile : Profile
{
    public SeasonProfile()
    {
        CreateMap<Routes.Models.Season, Data.DTOs.Season>().ReverseMap();
        CreateMap<Data.DTOs.Season, Routes.Models.Season>().ReverseMap();
    }
}


