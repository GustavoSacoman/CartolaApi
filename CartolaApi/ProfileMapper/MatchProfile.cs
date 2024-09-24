using AutoMapper;

namespace CartolaApi.ProfileMapper;

public class MatchProfile : Profile
{
    public MatchProfile()
    {
        CreateMap<Routes.Models.Match, Data.DTOs.Match>().ReverseMap();
        CreateMap<Data.DTOs.Match, Routes.Models.Match>().ReverseMap();
    }
}