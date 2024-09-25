using AutoMapper;

namespace CartolaApi.ProfileMapper;
public class TournamentProfile : Profile
{
    public TournamentProfile()
    {
        CreateMap<Routes.Models.Tournament, Data.DTOs.TournamentDTO>();
        CreateMap<Data.DTOs.TournamentDTO, Routes.Models.Tournament>();
    }
}


