using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WineLib.Models;

namespace WineLib.Repository
{
    public class WineRepository
    {
        private int _nextId = 1;
        private readonly List<Wine> _wineList = new List<Wine>();

        public WineRepository()
        {
            _wineList.Add(new() { Id = _nextId++, Manufacturer = "Brunello, Uggiano", Year = 2015, Price = 259, Rating = 4.2 });
            _wineList.Add(new() { Id = _nextId++, Manufacturer = "Gigino 80", Year = 2016, Price = 2019, Rating = 4.4 });
            _wineList.Add(new() { Id = _nextId++, Manufacturer = "Chianti, Caffaggio", Year = 2019, Price = 200, Rating = 3.7 });
            _wineList.Add(new() { Id = _nextId++, Manufacturer = "Crognolo", Year = 2014, Price = 249, Rating = 3.9 });
            _wineList.Add(new() { Id = _nextId++, Manufacturer = "Mate, Mantus", Year = 2023, Price = 169, Rating = 0 });

        }

        public Wine Add(Wine wine)
        {
            wine.Validate();       // svarer her til ModelState.IsValid
            wine.Id = _nextId++;   // Dette overwriter brugerens angivne id value. Id vil altid øges med +1
            _wineList.Add(wine);
            return wine;
        }

        public IEnumerable<Wine> GetAll()
        {
            IEnumerable<Wine> allWines = new List<Wine>(_wineList);
            return allWines;
        }


        public Wine? GetById(int id)
        {
            return _wineList.Find(b => b.Id == id);
        }

        public Wine? Remove(int id)
        {
            Wine? wine = GetById(id);
            if (wine == null)
                return null;

            _wineList.Remove(wine);
            return wine;
        }

        public Wine? Update(int id, Wine values)
        {
            values.Validate();                     // Er alt du har indtastet af værdier valid?
            Wine? existingWine = GetById(id);   // Findes en wine med det indtastede id?
            if (existingWine == null)
                return null;

            existingWine.Manufacturer = values.Manufacturer;  // Aha, wine findes.. Ny værdi.. go!
            existingWine.Year = values.Year;
            existingWine.Price = values.Price;
            existingWine.Rating = values.Rating;
            return existingWine;
        }
    }
}
