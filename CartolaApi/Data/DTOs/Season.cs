using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CartolaApi.Data.DTOs;
    public class Season
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public DateTime? StartDate { get; set; }
        [MaxLength(255)] 
        public DateTime? FinalDate { get; set; }

       
       // public List<Tournament>? Tournaments { get; set;} = new List<Tournament>(); 

       // [MaxLength(1000)]
       //public int? TournamentsId { get; set;}  


    public static Season CreateSeason(
        string name,
        DateTime? startDate,

        DateTime? finalDate
       // List<Tournament>? tournaments
        DateTime? finalDate,
       // int? tournamentsId

        )
    {
        return new Season
        {
            Name = name,
            StartDate = startDate,
            FinalDate = finalDate,

            //Tournaments = tournaments

            //TournamentsId = tournamentsId
        };
    }
}