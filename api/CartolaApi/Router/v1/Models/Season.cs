namespace CartolaApi.Router.v1.Models;
    public class Season
    {
        public required string Name { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime FinalDate { get; set; }
        public int? tournamentId { get; set;}
}
