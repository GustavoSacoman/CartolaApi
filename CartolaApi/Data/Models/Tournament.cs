namespace CartolaApi.Data.Models;

public class Tournament
{
    public int? Id { get; set; }
    public string TournamentName { get; set; }

    public List<Team>? Teams { get; set; }
    
    public static Tournament CreateTournament(string tournamentName, List<Team> teams)
    {
        return new Tournament
        {
            TournamentName = tournamentName,
            Teams = teams
        };
    }
}