namespace CartolaApi.Models
{
    public class Match
    {
        
        public int IdMatch { get; set; }
        public DateTime Date { get; set; }
        public string? Result { get; set; }
        public int IdTeam1 { get; set; }
        public int IdTeam2 { get; set; }
        public int IdTournament { get; set; }

    }
}