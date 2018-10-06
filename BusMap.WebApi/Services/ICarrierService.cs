using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto;

namespace BusMap.WebApi.Services
{
    public interface ICarrierService
    {
        Task<CarrierDto> GetCarrierAsync(int id);
        Task<IEnumerable<CarrierDto>> GetAllCarriersAsync();
        Task AddCarrierAsync(Carrier carrier);
        Task AddCarrierRangeAsync(IEnumerable<Carrier> carriers);
        Task RemoveCarrierAsync(CarrierDto carrier);
    }
}
