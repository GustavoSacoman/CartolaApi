using AutoMapper;

namespace CartolaApi.ProfileMapper;

public class MatchProfile : Profile
{
    public MatchProfile()
    {
        CreateMap<Router.v1.Models.Match, Data.DTOs.Match>().ReverseMap();
        CreateMap<Data.DTOs.Match, Router.v1.Models.Match>().ReverseMap();
        CreateMap<Router.v1.Models.UpdateMatch, Data.DTOs.Match>().ReverseMap();
        CreateMap<Data.DTOs.Match, Router.v1.Models.UpdateMatch>().ReverseMap();

    }
}