﻿using AutoMapper;

namespace CartolaApi.ProfileMapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Routes.Models.User, Data.DTOs.User>().ReverseMap();
        CreateMap<Data.DTOs.User, Routes.Models.User>().ReverseMap();
    }
}


