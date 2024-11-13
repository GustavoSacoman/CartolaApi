using AutoMapper;

namespace CartolaApi.ProfileMapper;

public class SeasonProfile : Profile
{
    public SeasonProfile()
    {
        CreateMap<Router.v1.Models.Season, Data.DTOs.Season>().ReverseMap();
        CreateMap<Data.DTOs.Season, Router.v1.Models.Season>().ReverseMap();
    }
}


