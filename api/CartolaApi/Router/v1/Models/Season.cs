namespace CartolaApi.Router.v1.Models;
    public class Season
    {   
        public required int Id{ get; set; }
        public required string Name { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime FinalDate { get; set; }  
}
