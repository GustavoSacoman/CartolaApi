using Newtonsoft.Json;
namespace CartolaApi.Router.v1.Models;
    public class Match
    {
        
        [JsonProperty(Required = Required.Default)]
        public int? IdMatch { get; set; }
        public DateTime Date { get; set; }
        [JsonProperty(Required = Required.Default)]
        public string? Result { get; set; }
        public int IdTeam1 { get; set; }
        public int IdTeam2 { get; set; }
        [JsonProperty(Required = Required.Default)]
        public int IdTournament { get; set; }

    }
