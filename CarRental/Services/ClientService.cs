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
    public class ClientService : IClient
    {

        private ApplicationDbContext _context;
        public ClientService(ApplicationDbContext context)
        {
            _context = context;
        }


        public void Add(Client newClient)
        {
            _context.Add(newClient);
            _context.SaveChanges();
        }

        public Client Get(int id)
        {
            return GetAll()
                .FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Client> GetAll()
        {
            return _context.Clients
               .Include(c => c.ClientCard);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int clientId)
        {
            var cardId = Get(clientId).ClientCard.Id;

            return _context.CheckoutHistories
                .Include(h => h.ClientCard)
                .Include(h => h.Car)
                .Where(h => h.ClientCard.Id == cardId)
                .OrderByDescending(h => h.CheckedOut);
        }

        public IEnumerable<Checkout> GetCheckouts(int clientId)
        {
            var cardId = Get(clientId).ClientCard.Id;


            return _context.Checkouts
                .Include(h => h.ClientCard)
                .Include(h => h.Car)
                .Where(h => h.ClientCard.Id == cardId);

        }
    }
}
