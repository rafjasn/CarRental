using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public class Checkout
    {
        public int Id { get; set; }
        [Required]
        public Car Car { get; set; }
        public ClientCard ClientCard { get; set; }
        public DateTime Since { get; set; }
        public DateTime Until { get; set; }

        public double NumberOfDays { get; set; }
        public double CheckoutCost { get; set; }
    }
}
