using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Interfaces
{
    public interface ICheckout
    {
        void Add(Checkout newCheckout);
        IEnumerable<Checkout> GetAll();
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int id);

        Checkout GetById(int checkoutId);
        Checkout GetLatestCheckout(int carId);
        string GetCurrentCheckoutClient(int carId);

        string GetCurrentCheckoutReturn(int carId);
        double GetNumberOfDays(int carId);
        double GetTotalCost(int carId);


        bool IsCheckedOut(int id);


        void CheckOutItem(int carId, int clientCardId, DateTime returnTime);
        void CheckInItem(int carId);
        void MarkDamaged(int carId);
        void MarkRepaired(int carId);
    }
}
