using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.Carriers;

namespace BusMap.WebApi.Services.Abstract
{
    public interface ICarrierService
    {
        Task<CarriersCarrierDto> GetCarrierAsync(int id);
        Task<CarriersCarrierDto> GetCarrierIncludeRoutesAsync(int id);
        Task<CarriersCarrierDto> GetCarrierIncludeRoutesBusStopsAsync(int id);
        Task<IEnumerable<CarriersCarrierDto>> GetAllCarriersAsync();
        Task AddCarrierAsync(Carrier carrier);
        Task AddCarrierRangeAsync(IEnumerable<Carrier> carriers);
        Task RemoveCarrierAsync(CarriersCarrierDto carrier);
    }
}
