using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;

namespace BusMap.WebApi.Repositories.Abstract
{
    public interface ICarrierRepository
    {
        Task<Carrier> GetCarrierAsync(int id);
        Task<Carrier> GetCarrierIncludeRoutesAsync(int id);
        Task<Carrier> GetCarrierIncludeRoutesBusStopsAsync(int id);

        Task<IEnumerable<Carrier>> GetAllCarriersAsync();
        Task<IEnumerable<Carrier>> GetAllCarriersIncludeRoutesAsync();
        Task<IEnumerable<Carrier>> GetAllCarriersIncludeRoutesBusStopsAsync();

        Task AddCarrierAsync(Carrier carrier);
        Task AddCarrierRangeAsync(IEnumerable<Carrier> carriers);
        Task RemoveCarrierAsync(Carrier carrier);
    }
}
