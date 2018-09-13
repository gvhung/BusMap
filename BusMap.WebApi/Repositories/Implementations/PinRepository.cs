using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.Data;
using BusMap.WebApi.Models;
using BusMap.WebApi.Repositories.Abstract;

namespace BusMap.WebApi.Repositories.Implementations
{
    public class PinRepository : IPinRepository
    {
        private readonly DatabaseContext _context;

        public PinRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Pin Get(int id)
            => _context.Pins.Find(id);

        public IEnumerable<Pin> GetAll()
            => _context.Pins;

        public void Add(Pin pin)
        {
            _context.Pins.Add(pin);
            _context.SaveChanges();
        }

        public void AddRange(IEnumerable<Pin> pins)
        {
            _context.Pins.AddRange(pins);
            _context.SaveChanges();
        }

        public void Remove(Pin pin)
        {
            _context.Pins.Remove(pin);
            _context.SaveChanges();
        }
    }
}
