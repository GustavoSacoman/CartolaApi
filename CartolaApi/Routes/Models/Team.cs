namespace CartolaApi.Routes.Models;


public class Team
{
    public string Name { get; set; }
    public List<Player> Players { get; set; } = new List<Player>();

    public int TournamentId { get; set; } 
    public Tournament? Tournament { get; set; }
}