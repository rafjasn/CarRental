using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.ViewModels.CarModels
{
    public class CarDetailsModel
    {
        public int CarId { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int Year { get; set; }
        public string Status { get; set; }
        public double Cost { get; set; }

        public string ClientName { get; set; }
        public string ReturnDate { get; set; }
        public double NumberOfDays { get; set; }
        public double TotalCost { get; set; }

        public Checkout LatestCheckout { get; set; }
        public IEnumerable<CheckoutHistory> CheckoutHistory { get; set; }
        public IEnumerable<Checkout> Checkouts { get; set; }





    }
}
