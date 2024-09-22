namespace CartolaApi.Data.Models;
public class Player
{
    public int? Id { get; set; }
    public string? NamePlayer { get; set; }
    public string? Position { get; set; }

    public int TeamId { get; set; } 
    public Team? PlayerTeam { get; set; } 
}