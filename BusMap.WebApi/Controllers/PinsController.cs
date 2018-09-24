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
        private readonly IBusStopRepository _pinRepository;

        public BusStopsController(IBusStopRepository pinRepository)
        {
            _pinRepository = pinRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pins = _pinRepository.GetAll();

            if (pins.ToList().Count == 0)
                return NotFound();

            return Ok(pins);
        }

        [HttpGet("{id}")]
        public IActionResult GetPin([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pin = _pinRepository.Get(id);
            if (pin == null)
                return NotFound();

            return Ok(pin);
        }

        [HttpPost]
        public IActionResult PostPin([FromBody] BusStop pin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _pinRepository.Add(pin);

            return CreatedAtAction("GetPin", new {id = pin.Id}, pin);
        }


    }
}