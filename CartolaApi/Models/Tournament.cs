
namespace CartolaApi.Models;

public class Tournament
{
    public int Id { get; set; }
    public string? TournamentName { get; set; }

    public List<Team> Teams { get; set; } = new List<Team>();
    public List<Match> Matches { get; set; } = new List<Match>();



}