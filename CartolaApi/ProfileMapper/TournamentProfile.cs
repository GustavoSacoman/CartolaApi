using AutoMapper;
using CartolaApi.Data.DTOs;
using CartolaApi.Routes.Models;

namespace CartolaApi.ProfileMapper;

public class TournamentProfile : Profile
{
    public TournamentProfile()
    {
        CreateMap<Routes.Models.Tournament, Data.DTOs.Tournament>().ReverseMap();
        CreateMap<Data.DTOs.Tournament, Routes.Models.Tournament>().ReverseMap();
    }
}


