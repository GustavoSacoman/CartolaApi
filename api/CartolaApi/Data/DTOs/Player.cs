using System.ComponentModel.DataAnnotations;

namespace CartolaApi.Data.DTOs;
public class Player
{
    public int? Id { get; set; } 
    [MaxLength(50)]
    [Required]
    public required string NamePlayer { get; set; }
    [MaxLength(50)]
    public string? Position { get; set; }

    [MaxLength(50)]
    public int? TeamId { get; set; } 
    
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