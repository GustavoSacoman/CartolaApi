using AutoMapper;

namespace CartolaApi.ProfileMapper;
public class TournamentProfile : Profile
{
    public TournamentProfile()
    {
        CreateMap<Router.v1.Models.Tournament, Data.DTOs.TournamentDTO>();
        CreateMap<Data.DTOs.TournamentDTO, Router.v1.Models.Tournament>();
    }
}


