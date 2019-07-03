using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.ViewModels.ClientModels
{
    public class ClientIndexModel
    {
        public IEnumerable<ClientDetailsModel> Clients { get; set; }
    }
}
