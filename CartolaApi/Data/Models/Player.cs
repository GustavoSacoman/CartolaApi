namespace CartolaApi.Data.Models;
public class Player
{
    public int? Id { get; set; }
    public required string NamePlayer { get; set; }
    public required string? Position { get; set; }

    public int? TeamId { get; set; } 
    public Team? PlayerTeam { get; set; } 
    
    public static Player CreatePlayer(string namePlayer, string? position, int? teamId)
    {
        if (namePlayer.Length > 50)
        {
            throw new Exception("Player name too long");
        }
        if (position != null && position.Length > 50)
        {
            throw new Exception("Position name too long");
        }
        return new Player
        {
            NamePlayer = namePlayer,
            Position = position,
            TeamId = teamId
        };
    }
}