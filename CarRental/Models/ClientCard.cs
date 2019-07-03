using System;
using System.Collections.Generic;

namespace CarRental.Models
{
    public class ClientCard
    {

        public int Id { get; set; }
        public decimal Fees { get; set; }
        public DateTime Created { get; set; }
        public virtual IEnumerable<Checkout> Checkouts { get; set; }
    }
}