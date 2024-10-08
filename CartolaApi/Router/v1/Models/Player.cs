﻿using Newtonsoft.Json;

namespace CartolaApi.Router.v1.Models;
public class Player
{
    public string NamePlayer { get; set; }
    public string? Position { get; set; }

    [JsonProperty(Required = Required.Default)]
    public int? TeamId { get; set; } 
}