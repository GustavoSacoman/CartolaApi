using AutoMapper;
using CartolaApi.Data.DTOs;
using CartolaApi.Routes.Models;

namespace CartolaApi.ProfileMapper;

public class TournamentProfile : Profile
{
    public TournamentProfile()
    {
        CreateMap<Tournament, TournamentDTO>();
        CreateMap<TournamentDTO, Tournament>();
    }
}


