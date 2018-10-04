using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusMap.WebApi.Dto;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusMap.WebApi.Helpers;

namespace BusMap.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteRepository _routeRepository;
        //private readonly IMapper _mapper;

        public RoutesController(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
            //_mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var routes = _routeRepository.GetAllRoutes()
                .ToList();

            if (routes.Count == 0)
                return NotFound();

            return Ok(routes);
        }

        [HttpGet("{id}")]
        public IActionResult GetRoute([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var route = _routeRepository
                .GetRouteIncludeBusStopsCarrier(id);
                //.ToRouteDto();

            if (route == null)
                return NotFound();
            //var mappedRoute = _mapper.Map<RouteModel>(route);

            return Ok(route);
        }

        [HttpPost]
        public IActionResult PostRoute([FromBody] Route route)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _routeRepository.AddRoute(route);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Route object is incomplete or contains wrong data.");
            }
            

            return CreatedAtAction("GetRoute", new {id = route.Id}, route);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoute([FromRoute] int id)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            //var routeToRemove = _routeRepository.GetRoute(id);
            //if (routeToRemove == null)
            //    return NotFound();

            //_routeRepository.RemoveRoute(routeToRemove);
            return Ok();
        }

    }
}