namespace CartolaApi.Routes.Models;
public class Tournament
{
    public int Id { get; set;}
    public string TournamentName { get; set; }

    public List<Team> Teams { get; set; }
}