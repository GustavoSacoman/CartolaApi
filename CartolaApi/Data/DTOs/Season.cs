namespace CartolaApi.Data.DTOs;
    public class Season
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime FinalDate { get; set; }
        public required List<Tournament> Tournaments { get; set;} = new List<Tournament>(); 
    }
