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
    public class CarService : ICar
    {
        private ApplicationDbContext _context;
        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Car newCar)
        {
            
            _context.Add(newCar);
            newCar.Status.Name = "Available";
            _context.SaveChanges();
        }

        public IEnumerable<Car> GetAll()
        {
            return _context.Cars
                .Include(c => c.Status);
        }

        public Car GetById(int id)
        {
            return GetAll()
                .FirstOrDefault(c => c.Id == id);
        }

        public string GetManufacturer(int id)
        {
            return _context.Cars.FirstOrDefault(c => c.Id == id).Manufacturer;
        }

        public string GetModel(int id)
        {
            return _context.Cars.FirstOrDefault(c => c.Id == id).Model;
        }

        public int GetYear(int id)
        {
            return _context.Cars.FirstOrDefault(c => c.Id == id).Year;
        }
    }
}
