using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public class CheckoutHistory
    {
        public int Id { get; set; }
        [Required]
        public Car Car { get; set; }
        [Required]
        public ClientCard ClientCard { get; set; }
        [Required]
        public DateTime CheckedOut { get; set; }
        public DateTime? CheckedIn { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
