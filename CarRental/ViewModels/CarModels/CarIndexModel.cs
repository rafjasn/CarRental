using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.ViewModels.CarModels
{
    public class CarIndexModel
    {
        public IEnumerable<CarIndexListingModel> Cars { get; set; }
    }
}
