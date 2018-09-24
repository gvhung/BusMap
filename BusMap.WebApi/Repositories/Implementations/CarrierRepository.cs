using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.Data;
using BusMap.WebApi.Models;
using BusMap.WebApi.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BusMap.WebApi.Repositories.Implementations
{
    public class CarrierRepository : ICarrierRepository
    {
        private readonly DatabaseContext _context;

        public CarrierRepository(DatabaseContext context)
        {
            _context = context;
        }


        public Carrier GetCarrier(int id)
            => _context.Carriers
                .Include(x => x.Routes)
                .First(x => x.Id == id);

        public IEnumerable<Carrier> GetAllCarriers()
            => _context.Carriers
                .Include(x => x.Routes);

        public void AddCarrier(Carrier carrier)
        {
            _context.Add(carrier);
            _context.SaveChanges();
        }

        public void AddCArrierRange(IEnumerable<Carrier> carriers)
        {
            _context.Carriers.AddRange(carriers);
            _context.SaveChanges();
        }

        public void RemoveCarrier(int id)
        {
            var carrierToRemove = GetCarrier(id);
            _context.Carriers.Remove(carrierToRemove);
            _context.SaveChanges();
        }
    }
}
