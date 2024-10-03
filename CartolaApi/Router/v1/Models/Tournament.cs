namespace CartolaApi.Router.v1.Models;
public class Tournament
{
    public int Id { get; set;}
    public string TournamentName { get; set; }

    public List<Team> Teams { get; set; }
}