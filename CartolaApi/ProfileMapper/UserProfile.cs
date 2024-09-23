using AutoMapper;
using CartolaApi.Data.DTOs;
using CartolaApi.Routes.Models;

namespace CartolaApi.ProfileMapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Routes.Models.User, Data.DTOs.User>().ReverseMap();
        CreateMap<Data.DTOs.User, Routes.Models.User>().ReverseMap();
    }
}
// ent é só fazer um desse pra cada DTO que vc tiver

