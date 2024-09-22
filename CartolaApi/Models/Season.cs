using System;
namespace CartolaApi.Models
{
    public class Season
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinalDate { get; set; }
        public List<Tournament> Tournaments { get; set;} = new List<Tournament>(); 
    }
}






