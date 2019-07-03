using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]

        public string Model { get; set; }
        [Required]

        public int Year { get; set; }

        public Status Status { get; set; }
        [Required]

        public double Cost { get; set; }

    }
}
