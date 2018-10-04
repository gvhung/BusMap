using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.Data;
using BusMap.WebApi.DatabaseModels;
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
            => _context.Carriers.FirstOrDefault(x => x.Id == id);

        public IEnumerable<Carrier> GetAllCarriers()
            => _context.Carriers;

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

        public void RemoveCarrier(Carrier carrier)
        {
            _context.Carriers.Remove(carrier);
            _context.SaveChanges();
        }
    }
}
