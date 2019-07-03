using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Interfaces
{
   public interface IClient
    {
        Client Get(int id);
        IEnumerable<Client> GetAll();
        void Add(Client newClient);

        IEnumerable<CheckoutHistory> GetCheckoutHistory(int clientId);
        IEnumerable<Checkout> GetCheckouts(int clientId);
    }
}
