namespace CartolaApi.Data.DTOs;

public class Team
{
    public int? Id { get; set; }
    public string Name { get; set; }

    public List<int>? PlayersId { get; set; }
    public int TournamentId { get; set; } 
    public Tournament? Tournament { get; set; }
    
    public static Team CreateTeam(string name, List<int> playersId)
    {
        return new Team
        {
            Name = name,
            PlayersId = playersId,
        };
    }
}