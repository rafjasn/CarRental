using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Interfaces
{
    public interface ICar
    {
        IEnumerable<Car> GetAll();
        Car GetById(int id);
        void Add(Car newCar);
        string GetManufacturer(int id);
        string GetModel(int id);
        int GetYear(int id);
    }
}
