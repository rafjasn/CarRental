using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.ViewModels.CheckoutModels
{
    public class CheckoutModel
    {
        public string ClientCardId { get; set; }
        public string Model { get; set; }
        public int CarId { get; set; }
        public string Manufacturer { get; set; }
        public bool IsCheckedOut { get; set; }
        public DateTime ReturnTIme { get; set; }

    }
}
