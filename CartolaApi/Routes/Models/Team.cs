namespace CartolaApi.Routes.Models;


public class Team
{

    public string Name { get; set; }
    public List<int> PlayersId { get; set; }
    
    public int TournamentId { get; set; } 

}