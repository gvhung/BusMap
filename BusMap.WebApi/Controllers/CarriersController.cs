using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.Models;
using BusMap.WebApi.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusMap.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarriersController : ControllerBase
    {
        private readonly ICarrierRepository _carrierRepository;

        public CarriersController(ICarrierRepository carrierRepository)
        {
            _carrierRepository = carrierRepository;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carriers = _carrierRepository.GetAllCarriers().ToList();
            if (carriers.Count == 0)
                return NotFound();

            return Ok(carriers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCarrier([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carrier = _carrierRepository.GetCarrier(id);

            if (carrier == null)
                return NotFound();

            return Ok(carrier);
        }

        [HttpPost]
        public IActionResult PostCarrier([FromBody] Carrier carrier)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _carrierRepository.AddCarrier(carrier);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Carrier object is incomplete or contains wrong data.");
            }
            

            return CreatedAtAction("GetCarrier", new {id = carrier.Id}, carrier);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCarrier([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carrierToDelete = _carrierRepository.GetCarrier(id);
            if (carrierToDelete == null)
                return NotFound();

            _carrierRepository.RemoveCarrier(carrierToDelete);

            return Ok();
        }

    }
}