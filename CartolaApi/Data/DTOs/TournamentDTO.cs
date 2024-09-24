using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using CartolaApi.Routes.Models;

namespace CartolaApi.Data.DTOs;

public class TournamentDTO
{
    public int? Id { get; set; }
   
    public string TournamentName { get; set; }

    //public List<int> TeamsIds { get; set; }

    public Tournament ConvertTournament(TournamentDTO tournamentDTO)
    {
        if (tournamentDTO == null)
        {
            throw new Exception("Tournament not found");
        }

        // Se TeamsIds não estiver nulo, você pode criar a lista de Team a partir do DTO aqui
       

        return new Tournament
        {
            Id = tournamentDTO.Id ?? 0, // Define um ID padrão caso seja nulo
            TournamentName = tournamentDTO.TournamentName
        };
    }
    
}