namespace CartolaApi.Routes.Models;


public class Tournament
{
    public string TournamentName { get; set; }

    public List<Team> Teams { get; set; } = new List<Team>();
}