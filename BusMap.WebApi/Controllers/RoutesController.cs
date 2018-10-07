using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
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
        public async Task<IActionResult> GetRoutes()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var routes = await _routeService
                .GetAllRoutesAsync();

            if (routes.ToList().Count == 0)
                return NotFound();

            return Ok(routes);
        }

        [HttpGet("busStops")]
        public async Task<IActionResult> GetRoutesIncludeBusStops()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var routes = await _routeService
                .GetAllRoutesIncludeBusStopsAsync();

            if (routes.ToList().Count == 0)
                return NotFound();

            return Ok(routes);
        }

        [HttpGet("carrier")]
        public async Task<IActionResult> GetRoutesIncludeCarrier()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var routes = await _routeService
                .GetAllRoutesIncludeCarrierAsync();

            if (routes.ToList().Count == 0)
                return NotFound();

            return Ok(routes);
        }

        [HttpGet("busStopsCarrier")]
        [HttpGet("carrierBusStops")]
        public async Task<IActionResult> GetRoutesIncludeBusStopsCarrier()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var routes = await _routeService
                .GetAllRoutesIncludeBusStopsCarrierAsync();

            if (routes.ToList().Count == 0)
                return NotFound();

            return Ok(routes);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRoute([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var route = await _routeService
                .GetRouteAsync(id);

            if (route == null)
                return NotFound();

            return Ok(route);
        }

        [HttpGet("{id:int}/busStops")]
        public async Task<IActionResult> GetRouteIncludeBusStops([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var route = await _routeService
                .GetRouteIncludeBusStopsAsync(id);

            if (route == null)
                return NotFound();

            return Ok(route);
        }

        [HttpGet("{id:int}/carrier")]
        public async Task<IActionResult> GetRouteIncludeCarrier([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var route = await _routeService
                .GetRouteIncludeCarrierAsync(id);

            if (route == null)
                return NotFound();

            return Ok(route);
        }

        [HttpGet("{id:int}/busStopsCarrier")]
        [HttpGet("{id:int}/carrierBusStops")]
        public async Task<IActionResult> GetRouteIncludeBusStopsCarriers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var route = await _routeService
                .GetRouteIncludeBusStopsCarrierAsync(id);

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