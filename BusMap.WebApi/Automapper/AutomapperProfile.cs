using AutoMapper;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.BusStops;
using BusMap.WebApi.Dto.Carriers;
using BusMap.WebApi.Dto.Queues;
using BusMap.WebApi.Dto.Routes;

namespace BusMap.WebApi.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Carrier, CarriersCarrierDto>();
            CreateMap<BusStop, CarriersBusStopDto>();
            CreateMap<Route, CarriersRouteDto>();

            CreateMap<Carrier, BusStopsCarrierDto>();
            CreateMap<BusStop, BusStopsBusStopDto>();
            CreateMap<Route, BusStopsRouteDto>();

            CreateMap<Carrier, RoutesCarrierDto>();
            CreateMap<BusStop, RoutesBusStopDto>();
            CreateMap<Route, RoutesRouteDto>();

            //CreateMap<RouteQueued, RoutesRouteDto>()
            //    .ForMember(vm => vm.Carrier, m => m.MapFrom(x => x.CarrierQueued))
            //    .ForMember(dm => dm.BusStops, options => options.MapFrom(x => x.BusStopsQueued));
            //CreateMap<BusStopQueued, RoutesBusStopDto>();
            //CreateMap<CarrierQueued, RoutesCarrierDto>();


            CreateMap<RouteQueued, QueuesRouteDto>();
            CreateMap<BusStopQueued, QueuesBusStopDto>();
            CreateMap<CarrierQueued, QueuesCarrierDto>();

        }
    }
}
