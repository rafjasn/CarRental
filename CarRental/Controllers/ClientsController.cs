using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Interfaces;
using CarRental.ViewModels.ClientModels;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class ClientsController : Controller
    {
        private IClient _client;

        public ClientsController(IClient client)
        {
            _client = client;
        }



        public IActionResult Index()
        {

            var clients = _client.GetAll();

            var clientListings = clients.Select(c => new ClientDetailsModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                ClientCardId = c.ClientCard.Id,
                OverdueFees = c.ClientCard.Fees,
                

            }).ToList();


            var model = new ClientIndexModel()
            {
                Clients = clientListings
            };

            return View(model);
        }


        public IActionResult Details(int id)
        {
            var client = _client.Get(id);

            var model = new ClientDetailsModel
            {
                Id = client.Id,
                LastName = client.LastName,
                FirstName = client.FirstName,
                Address = client.Address,
                MemberSince = client.ClientCard.Created,
                OverdueFees = client.ClientCard.Fees,
                ClientCardId = client.ClientCard.Id,
                Telephone = client.PhonehoneNumber,
                CarsCheckedOut = _client.GetCheckouts(id),
                CheckoutHistory = _client.GetCheckoutHistory(id)


            };

            return View(model);
        }


    }
}