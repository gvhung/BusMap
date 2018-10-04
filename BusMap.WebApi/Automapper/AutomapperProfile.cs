using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto;
using BusMap.WebApi.Models;

namespace BusMap.WebApi.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Route, RouteModel>().ReverseMap();
            CreateMap<Carrier, CarrierModel>().ReverseMap();
            CreateMap<BusStop, BusStopModel>().ReverseMap();
        }
    }
}
