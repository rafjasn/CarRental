using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;
using CarRental.Interfaces;
using CarRental.ViewModels.CarModels;
using CarRental.ViewModels.CheckoutModels;

namespace CarRental.Controllers
{
    public class CarsController : Controller
    {
        private ICar _cars;
        private ICheckout _checkouts;

        private ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context, ICar cars, ICheckout checkouts)
        {
            _context = context;
            _cars = cars;
            _checkouts = checkouts;
        }

        // /Cars/Index

        public IActionResult Index()
        {
            var carModels = _cars.GetAll();
            var listingResult = carModels
                .Select(result => new CarIndexListingModel
                {
                    Id = result.Id,
                    Manufacturer = _cars.GetManufacturer(result.Id),
                    Model = _cars.GetModel(result.Id),
                    Year = _cars.GetYear(result.Id).ToString()

                });

            var model = new CarIndexModel()
            {
                Cars = listingResult
            };


            return View(model);

        }





        public IActionResult Details(int id)
        {
            var car = _cars.GetById(id);



            var model = new CarDetailsModel
            {
                CarId = id,
                Model = car.Model,
                Manufacturer = car.Manufacturer,
                Cost = car.Cost,
                Status = car.Status.Name,
                CheckoutHistory = _checkouts.GetCheckoutHistory(id),
                LatestCheckout = _checkouts.GetLatestCheckout(id),
                ClientName = _checkouts.GetCurrentCheckoutClient(id),
                ReturnDate = _checkouts.GetCurrentCheckoutReturn(id),
                NumberOfDays = _checkouts.GetNumberOfDays(id),
                TotalCost = _checkouts.GetTotalCost(id),


            };

            return View(model);

        }



        public IActionResult Checkout(int id)
        {
            var car = _cars.GetById(id);
            var model = new CheckoutModel
            {
                CarId = id,
                Manufacturer = car.Manufacturer,
                Model = car.Model,
                ClientCardId = "",

                IsCheckedOut = _checkouts.IsCheckedOut(id)
            };
            return View(model);
        }

        public IActionResult CheckIn(int id)
        {
            _checkouts.CheckInItem(id);
            return RedirectToAction("Details", new { id = id });
        }

        public IActionResult MarkDamaged(int id)
        {
            _checkouts.MarkDamaged(id);
            return RedirectToAction("Details", new { id = id });

        }
        public IActionResult MarkRepaired(int id)
        {
            _checkouts.MarkRepaired(id);
            return RedirectToAction("Details", new { id = id });

        }




        [HttpPost]
        public IActionResult PlaceCheckout(int carId, int clientCardId, DateTime returnTime)
        {
            _checkouts.CheckOutItem(carId, clientCardId, returnTime);
            return RedirectToAction("Details", new { id = carId });
        }



   





        //// GET: Cars
        //public async Task<IActionResult> Index()
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Cars.ToListAsync());
        //}

        //// GET: Cars/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var car = await _context.Cars
        //        .Include(s => s.Status)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (car == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(car);
        //}

        //// GET: Cars/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Cars/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Manufacturer,Model,Year,Cost")] Car car)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(car);


        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(car);
        //}

        //// GET: Cars/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var car = await _context.Cars.FindAsync(id);
        //    if (car == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(car);
        //}

        //// POST: Cars/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Manufacturer,Model,Year,Cost")] Car car)
        //{
        //    if (id != car.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(car);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CarExists(car.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(car);
        //}

        //// GET: Cars/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var car = await _context.Cars
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (car == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(car);
        //}

        //// POST: Cars/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var car = await _context.Cars.FindAsync(id);
        //    _context.Cars.Remove(car);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CarExists(int id)
        //{
        //    return _context.Cars.Any(e => e.Id == id);
        //}
    }
}
