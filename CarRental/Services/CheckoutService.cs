using CarRental.Data;
using CarRental.Interfaces;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Services
{
    public class CheckoutService : ICheckout
    {
        ApplicationDbContext _context;
        public CheckoutService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Checkout newCheckout)
        {
            _context.Add(newCheckout);
            _context.SaveChanges();
        }

        public void CheckInItem(int carId)
        {
            var now = DateTime.Now;
            var item = _context.Cars
                .FirstOrDefault(c => c.Id == carId);

            // remove any existing checkouts on the item

            RemoveExistingCheckouts(carId);

            
            
            // close any existing checkout history

            CloseExistingCheckoutHistory(carId, now);


            //update the item status to available

            UpdateCarStatus(carId, "Available");
            _context.SaveChanges();



        }

        private void UpdateCarStatus(int carId, string v)
        {
            var item = _context.Cars
                .FirstOrDefault(c => c.Id == carId);
            _context.Update(item);
            item.Status = _context.Statuses
                .FirstOrDefault(s => s.Name == v);

        }

        private void CloseExistingCheckoutHistory(int carId, DateTime now)
        {
            var history = _context.CheckoutHistories
                .FirstOrDefault(h => h.Car.Id == carId && h.CheckedIn == null);

            if (history != null)
            {
                _context.Update(history);
                // var retDate = _context.Checkout.FirstOrDefault(t => t.Id == assetId).Until;
                history.CheckedIn = now;
                //   history.ReturnDate = retDate;

            }

        }

        private void RemoveExistingCheckouts(int carId)
        {
            var checkout = _context.Checkouts
                .FirstOrDefault(c => c.Car.Id == carId);

            if (checkout != null)
            {
                _context.Remove(checkout);
            }



        }
       // ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void CheckOutItem(int carId, int clientCardId, DateTime returnTime)
        {
            if (IsCheckedOut(carId))
            {
                return;
            }

            var item = _context.Cars
                .FirstOrDefault(c => c.Id == carId);

            UpdateCarStatus(carId, "Rented Out");


            var client = _context.ClientCards
                .Include(c => c.Checkouts)
                .FirstOrDefault(c => c.Id == clientCardId);

            var now = DateTime.Now;
            var cost = item.Cost;
            var checkout = new Checkout
            {
                Car = item,
                Since = now,
                ClientCard = client,
                Until = now,
                NumberOfDays = (returnTime - now).TotalDays,
                CheckoutCost = cost * (returnTime - now).TotalDays

            };

            _context.Add(checkout);
            var checkoutHistory = new CheckoutHistory
            {
                CheckedOut = now,
                Car = item,
                ClientCard = client,
                ReturnDate = returnTime

            };
            _context.Add(checkoutHistory);
            _context.SaveChanges();





        }

        public bool IsCheckedOut(int carId)
        {
            return _context.Checkouts
                .Where(c => c.Car.Id == carId)
                .Any();
        }

        public IEnumerable<Checkout> GetAll()
        {
            return _context.Checkouts;
        }

        public Checkout GetById(int checkoutId)
        {
            return GetAll().FirstOrDefault(c => c.Id == checkoutId);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int id)
        {
            return _context.CheckoutHistories
                .Include(c => c.Car)
                .Include(c => c.ClientCard)
                .Where(c => c.Car.Id == id);
        }

        public string GetCurrentCheckoutClient(int carId)
        {
            var checkout = GetCheckoutByCarId(carId);

            if (checkout == null)
            {
                return "";
            }

            var cardId = checkout.ClientCard.Id;
            var client = _context.Clients
                .Include(p => p.ClientCard)
                .FirstOrDefault(p => p.ClientCard.Id == cardId);

            return client.FirstName + " " + client.LastName;





        }

        private Checkout GetCheckoutByCarId(int carId)
        {
            return _context.Checkouts
                .Include(c => c.Car)
                .Include(c => c.ClientCard)
                .FirstOrDefault(c => c.Car.Id == carId);
        }

        public string GetCurrentCheckoutReturn(int carId)
        {
            var checkout = GetCheckoutByCarId(carId);
            if (checkout == null)
            {
                return "";
            }
            var cardId = checkout.ClientCard.Id;
            var client = _context.Checkouts
                .Include(c => c.ClientCard)
                .FirstOrDefault(c => c.ClientCard.Id == cardId);
            return client.Until.ToString();


        }

        public Checkout GetLatestCheckout(int carId)
        {
            return _context.Checkouts.Where(c => c.Car.Id == carId)
                .OrderByDescending(c => c.Since)
                .FirstOrDefault();
        }

        public double GetNumberOfDays(int carId)
        {
            var checkout = GetCheckoutByCarId(carId);
            if (checkout == null)
            {
                return 0;
            }
            var cardId = checkout.ClientCard.Id;
            var client = _context.Checkouts
                .Include(c => c.ClientCard)
                .FirstOrDefault(c => c.ClientCard.Id == cardId);
            return client.NumberOfDays;
        }

        public double GetTotalCost(int carId)
        {
            var checkout = GetCheckoutByCarId(carId);
            if (checkout == null)
            {
                return 0;
            }
            var cardId = checkout.ClientCard.Id;
            var client = _context.Checkouts
                .Include(c => c.ClientCard)
                .FirstOrDefault(c => c.ClientCard.Id == cardId);
            return client.CheckoutCost;
        }

        public void MarkDamaged(int carId)
        {
            UpdateCarStatus(carId, "Damaged");

            _context.SaveChanges();
        }

        public void MarkRepaired(int carId)
        {
            var now = DateTime.Now;


            UpdateCarStatus(carId, "Available");

            RemoveExistingCheckouts(carId);


            CloseExistingCheckoutHistory(carId, now);


            _context.SaveChanges();
        }
    }
}
