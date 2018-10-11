using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.Routes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusMap.WebApi.Services.Abstract;

namespace BusMap.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RoutesController(IRouteService routeService)
        {
            _routeService = routeService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllRoutes()
            => await GetAllRoutesFunc(async () => await _routeService.GetAllRoutesAsync());

        [HttpGet("busStops")]
        public async Task<IActionResult> GetAllRoutesIncludeBusStops()
            => await GetAllRoutesFunc(async () => await _routeService.GetAllRoutesIncludeBusStopsAsync());

        [HttpGet("carrier")]
        public async Task<IActionResult> GetAllRoutesIncludeCarrier()
            => await GetAllRoutesFunc(async () => await _routeService.GetAllRoutesIncludeCarrierAsync());

        [HttpGet("busStopsCarrier")]
        [HttpGet("carrierBusStops")]
        public async Task<IActionResult> GetAllRoutesIncludeBusStopsCarrier()
            => await GetAllRoutesFunc(async () => await _routeService.GetAllRoutesIncludeBusStopsCarrierAsync());


        public async Task<IActionResult> GetAllRoutesFunc(Func<Task<IEnumerable<RoutesRouteDto>>> getAllRoutesFunc)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var routes = await getAllRoutesFunc();

            if (routes.ToList().Count == 0)
                return NotFound();

            return Ok(routes);
        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRoute([FromRoute] int id)
            => await GetRouteFunc(id, async x => await _routeService.GetRouteAsync(id));

        [HttpGet("{id:int}/busStops")]
        public async Task<IActionResult> GetRouteIncludeBusStops([FromRoute] int id)
            => await GetRouteFunc(id, async x => await _routeService.GetRouteIncludeBusStopsAsync(id));

        [HttpGet("{id:int}/carrier")]
        public async Task<IActionResult> GetRouteIncludeCarrier([FromRoute] int id)
            => await GetRouteFunc(id, async x => await _routeService.GetRouteIncludeCarrierAsync(id));

        [HttpGet("{id:int}/busStopsCarrier")]
        [HttpGet("{id:int}/carrierBusStops")]
        public async Task<IActionResult> GetRouteIncludeBusStopsCarriers([FromRoute] int id)
            => await GetRouteFunc(id, async x => await _routeService.GetRouteIncludeBusStopsCarrierAsync(id));


        public async Task<IActionResult> GetRouteFunc(int id, Func<int, Task<RoutesRouteDto>> getRouteFunc)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var route = await getRouteFunc(id);

            if (route == null)
                return NotFound();

            return Ok(route);
        }

        

        [HttpPost]
        public async Task<IActionResult> PostRoute([FromBody] Route route)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _routeService.AddRouteAsync(route);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Route object is incomplete or contains wrong data.");
            }
            
            return CreatedAtAction("GetRoute", new {id = route.Id}, route);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var routeToRemove = await _routeService.GetRouteAsync(id);
            if (routeToRemove == null)
                return NotFound();

            await _routeService.RemoveRouteAsync(routeToRemove);
            return Ok();
        }

    }
}