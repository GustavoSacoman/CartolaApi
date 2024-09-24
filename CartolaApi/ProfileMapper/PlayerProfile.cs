using AutoMapper;

namespace CartolaApi.ProfileMapper;

public class PlayerProfile : Profile
{
    public PlayerProfile()
    {
        CreateMap<Routes.Models.Player, Data.DTOs.Player>().ReverseMap();
        CreateMap<Data.DTOs.Player, Routes.Models.Player>().ReverseMap();
    }
}


