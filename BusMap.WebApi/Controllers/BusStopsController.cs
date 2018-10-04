using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusMap.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusStopsController : ControllerBase
    {
        private readonly IBusStopRepository _busStopRepository;

        public BusStopsController(IBusStopRepository busStopRepository)
        {
            _busStopRepository = busStopRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var busStops = _busStopRepository.GetAllBusStops().ToList();
            if (busStops.Count == 0)
                return NotFound();

            return Ok(busStops);
        }

        [HttpGet("{id}")]
        public IActionResult GetBusStop([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var busStop = _busStopRepository.GetBusStop(id);
            if (busStop == null)
                return NotFound();

            return Ok(busStop);
        }

        [HttpPost]
        public IActionResult PostBusStop([FromBody] BusStop busStop)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _busStopRepository.AddBusStop(busStop);
            }
            catch (DbUpdateException)
            {
                return BadRequest("BusStop object is incomplete or contains wrong data.");
            };
            
            return CreatedAtAction("GetBusStop", new {id = busStop.Id}, busStop);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBusStop([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var busStopToRemove = _busStopRepository.GetBusStop(id);
            if (busStopToRemove == null)
                return NotFound();

            _busStopRepository.RemoveBusStop(busStopToRemove);
            return Ok();
        }


    }
}