using System.ComponentModel.DataAnnotations;

namespace CartolaApi.Data.DTOs;
    public class Match
    {
        
        public int? IdMatch { get; set; }
        [MaxLength(50)]
        [Required]
        public DateTime Date { get; set; }
        [MaxLength(50)]
        public string? Result { get; set; }
        [MaxLength(50)]
        [Required]
        public int IdTeam1 { get; set; }
        [MaxLength(50)]
        [Required]
        public int IdTeam2 { get; set; }
        [MaxLength(50)]
        public int? IdTournament { get; set; }
    
        public static Match CreateMatch(DateTime date, string result, int idTeam1, int idTeam2, int idTournament)
        {
            return new Match
            {
                Date = date,
                Result = result,
                IdTeam1 = idTeam1,
                IdTeam2 = idTeam2,
                IdTournament = idTournament
            };
        }
        
    }
