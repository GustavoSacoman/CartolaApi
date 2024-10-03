using AutoMapper;

namespace CartolaApi.ProfileMapper;

public class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<Router.v1.Models.Team, Data.DTOs.Team>().ReverseMap();
        CreateMap<Data.DTOs.Team, Router.v1.Models.Team>().ReverseMap();
    }
}


