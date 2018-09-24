using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.Models;

namespace BusMap.WebApi.Repositories.Abstract
{
    public interface IBusStopRepository
    {
        BusStop Get(int id);
        IEnumerable<BusStop> GetAll();
        void Add(BusStop busStop);
        void AddRange(IEnumerable<BusStop> busStops);
        void Remove(int id);
    }
}
