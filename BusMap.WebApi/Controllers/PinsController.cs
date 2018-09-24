using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.Models;
using BusMap.WebApi.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusMap.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusStopsController : ControllerBase
    {
        private readonly IBusStopRepository __busStopRepository;

        public BusStopsController(IBusStopRepository busStopRepository)
        {
            __busStopRepository = busStopRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pins = __busStopRepository.GetAllBusStops();

            if (pins.ToList().Count == 0)
                return NotFound();

            return Ok(pins);
        }

        [HttpGet("{id}")]
        public IActionResult GetPin([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pin = __busStopRepository.GetBusStop(id);
            if (pin == null)
                return NotFound();

            return Ok(pin);
        }

        [HttpPost]
        public IActionResult PostPin([FromBody] BusStop pin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            __busStopRepository.AddBusStop(pin);

            return CreatedAtAction("GetPin", new {id = pin.Id}, pin);
        }


    }
}