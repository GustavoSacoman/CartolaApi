using AutoMapper;

namespace CartolaApi.ProfileMapper;

public class PlayerProfile : Profile
{
    public PlayerProfile()
    {
        CreateMap<Router.v1.Models.Player, Data.DTOs.Player>().ReverseMap();
        CreateMap<Data.DTOs.Player, Router.v1.Models.Player>().ReverseMap();
    }
}


