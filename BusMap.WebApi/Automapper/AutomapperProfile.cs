using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.BusStops;
using BusMap.WebApi.Dto.Carriers;
using BusMap.WebApi.Models;

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
        }
    }
}
