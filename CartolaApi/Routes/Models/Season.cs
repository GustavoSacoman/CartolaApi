namespace CartolaApi.Routes.Models;
    public class Season
    {
        public required string Name { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime FinalDate { get; set; }
        public required List<Tournament> Tournaments { get; set;} = new List<Tournament>(); 
    }
// isso, mas eu vou muda
