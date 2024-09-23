using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CartolaApi.Data.DTOs;

public class Tournament
{
    public int? Id { get; set; }
    [MaxLength(255)]
    [Required]
    public string TournamentName { get; set; }

    [MaxLength(1000)]
    public List<Team>? Teams { get; set; }
    
    public static Tournament CreateTournament(string tournamentName, List<Team>? teams)
    {
        return new Tournament
        {
            TournamentName = tournamentName,
            Teams = teams
        };
    }
}