using AutoMapper;

namespace CartolaApi.ProfileMapper;

public class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<Routes.Models.Team, Data.DTOs.Team>().ReverseMap();
        CreateMap<Data.DTOs.Team, Routes.Models.Team>().ReverseMap();
    }
}


