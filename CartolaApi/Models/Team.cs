public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Player> Players { get; set; } = new List<Player>();
}