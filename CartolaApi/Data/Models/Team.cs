namespace CartolaApi.Data.Models;

public class Team
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public List<Player> Players { get; set; } = new List<Player>();

    public int TournamentId { get; set; } 
    public Tournament? Tournament { get; set; }
}