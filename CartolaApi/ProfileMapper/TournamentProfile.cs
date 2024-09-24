using AutoMapper;

namespace CartolaApi.ProfileMapper;

public class TournamentProfile : Profile
{
    public TournamentProfile()
    {
        CreateMap<Tournament, TournamentDTO>();
        CreateMap<TournamentDTO, Tournament>();
    }
}


