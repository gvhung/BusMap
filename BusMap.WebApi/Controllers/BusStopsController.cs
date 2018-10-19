using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.BusStops;
using BusMap.WebApi.Repositories.Abstract;
using BusMap.WebApi.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusMap.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusStopsController : ControllerBase
    {
        private readonly IBusStopService _busStopService;

        public BusStopsController(IBusStopService busStopService)
        {
            _busStopService = busStopService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBusStops()
            => await GetAllBusStopsFunc(async () => await _busStopService.GetAllBusStopsAsync());

        [HttpGet("route")]
        public async Task<IActionResult> GetAllBusStopsIncludeRoute()
            => await GetAllBusStopsFunc(async () => await _busStopService.GetAllBusStopsIncludeRouteAsync());

        [HttpGet("routeCarrier")]
        public async Task<IActionResult> GetAllBusStopsIncludeRouteCarrier()
            => await GetAllBusStopsFunc(async () => await _busStopService.GetAllBusStopsIncludeRouteCarrierAsync());

        public async Task<IActionResult> GetAllBusStopsFunc(Func<Task<IEnumerable<BusStopsBusStopDto>>> getAllBusStopsFunc)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var busStops = await getAllBusStopsFunc();

            if (busStops == null || busStops.Count() < 1)
                return NotFound();

            return Ok(busStops);
        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBusStop([FromRoute] int id)
            => await GetBusStopFunc(id, async x => await _busStopService.GetBusStopAsync(id));

        [HttpGet("{id:int}/route")]
        public async Task<IActionResult> GetBusStopIncludeRoute([FromRoute] int id)
            => await GetBusStopFunc(id, async x => await _busStopService.GetBusStopIncludeRouteAsync(id));

        [HttpGet("{id:int}/routeCarrier")]
        public async Task<IActionResult> GetBusStopIncludeRouteCarrier([FromRoute] int id)
            => await GetBusStopFunc(id, async x => await _busStopService.GetBusStopIncludeRouteCarrierAsync(id));


        public async Task<IActionResult> GetBusStopFunc(int id, Func<int, Task<BusStopsBusStopDto>> getBusStopsFunc)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var busStop = await getBusStopsFunc(id);

            if (busStop == null)
                return NotFound();

            return Ok(busStop);
        }



        [HttpPost]
        public async Task<IActionResult> PostBusStop([FromBody] BusStop busStop)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _busStopService.AddBusStopAsync(busStop);
            }
            catch (DbUpdateException)
            {
                return BadRequest("BusStop object is incomplete or contains wrong data.");
            };

            return CreatedAtAction("GetBusStop", new { id = busStop.Id }, busStop);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusStop([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var busStopToRemove = await _busStopService.GetBusStopAsync(id);
            if (busStopToRemove == null)
                return NotFound();

            await _busStopService.RemoveBusStopAsync(busStopToRemove);
            return Ok();
        }


    }
}