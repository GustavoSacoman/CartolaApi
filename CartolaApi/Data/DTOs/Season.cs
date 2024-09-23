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
        [MaxLength(1000)]
        public List<Tournament>? Tournaments { get; set;} = new List<Tournament>(); 

    public static Season CreateSeason(
        string name,
        DateTime? startDate,
        DateTime? finalDate,
        List<Tournament>? tournaments
        )
    {
        return new Season
        {
            Name = name,
            StartDate = startDate,
            FinalDate = finalDate,
            Tournaments = tournaments
        };
    }
}