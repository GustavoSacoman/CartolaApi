using AutoMapper;

namespace CartolaApi.ProfileMapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Router.v1.Models.User, Data.DTOs.User>().ReverseMap();
        CreateMap<Data.DTOs.User, Router.v1.Models.User>().ReverseMap();
        CreateMap<Router.v1.Models.UserLogin, Data.DTOs.User>();
        CreateMap<Data.DTOs.User, Router.v1.Models.UserLogin>().ReverseMap();
    }
}


