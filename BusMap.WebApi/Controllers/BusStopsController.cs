using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
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
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var busStops = await _busStopService.GetAllBusStopsAsync();
            if (busStops.ToList().Count == 0)
                return NotFound();

            return Ok(busStops);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusStop([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var busStop = await _busStopService.GetBusStopAsync(id);
            if (busStop == null)
                return NotFound();

            return Ok(busStop);
        }

        [HttpGet("{id}/route")]
        public async Task<IActionResult> GetBusStopIncludeRoute([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var busStop = await _busStopService.GetBusStopIncludeRouteAsync(id);
            if (busStop == null)
                return NotFound();

            return Ok(busStop);
        }

        [HttpGet("{id}/routeCarrier")]
        public async Task<IActionResult> GetBusStopIncludeRouteCarrier([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var busStop = await _busStopService.GetBusStopIncludeRouteCarrierAsync(id);
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
            
            return CreatedAtAction("GetBusStop", new {id = busStop.Id}, busStop);
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