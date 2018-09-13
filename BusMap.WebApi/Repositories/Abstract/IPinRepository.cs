using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.Models;

namespace BusMap.WebApi.Repositories.Abstract
{
    public interface IPinRepository
    {
        Pin Get(int id);
        IEnumerable<Pin> GetAll();
        void Add(Pin pin);
        void AddRange(IEnumerable<Pin> pins);
        void Remove(Pin pin);
    }
}
