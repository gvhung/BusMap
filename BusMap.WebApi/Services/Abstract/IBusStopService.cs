using System.Collections.Generic;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.BusStops;

namespace BusMap.WebApi.Services.Abstract
{
    public interface IBusStopService
    {
        Task<BusStopsBusStopDto> GetBusStopAsync(int id);
        Task<BusStopsBusStopDto> GetBusStopIncludeRouteAsync(int id);
        Task<BusStopsBusStopDto> GetBusStopIncludeRouteCarrierAsync(int id);
        Task<BusStopsBusStopDto> GetBusStopIncludeAllAsync(int id);

        Task<IEnumerable<BusStopsBusStopDto>> GetAllBusStopsAsync();
        Task<IEnumerable<BusStopsBusStopDto>> GetAllBusStopsIncludeRouteAsync();
        Task<IEnumerable<BusStopsBusStopDto>> GetAllBusStopsIncludeRouteCarrierAsync();

        Task AddBusStopAsync(BusStop busStop);
        Task AddBusStopsRangeAsync(IEnumerable<BusStop> busStops);

        Task RemoveBusStopAsync(BusStopsBusStopDto busStop);
    }
}
