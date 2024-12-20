﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations.Schema;

namespace CartolaApi.Data.DTOs;
    public class Season
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int Id{ get; set; }
        public required string Name { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime FinalDate { get; set; }    
}