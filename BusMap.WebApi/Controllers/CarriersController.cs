using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusMap.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarriersController : ControllerBase
    {
        private readonly ICarrierService _carrierService;

        public CarriersController(ICarrierService carrierService)
        {
            _carrierService = carrierService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carriers = await _carrierService.GetAllCarriersAsync();
            if (carriers.ToList().Count == 0)
                return NotFound();

            return Ok(carriers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarrier([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carrier = await _carrierService.GetCarrierAsync(id);

            if (carrier == null)
                return NotFound();

            return Ok(carrier);
        }

        [HttpGet("{id}/routes")]
        public async Task<IActionResult> GetCarrierIncludeRoutes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carrier = await _carrierService.GetCarrierIncludeRoutesAsync(id);

            if (carrier == null)
                return NotFound();

            return Ok(carrier);
        }

        [HttpGet("{id}/routesBusStops")]
        public async Task<IActionResult> GetCarrierIncludeRoutesBusStops([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carrier = await _carrierService.GetCarrierIncludeRoutesBusStopsAsync(id);

            if (carrier == null)
                return NotFound();

            return Ok(carrier);
        }

        [HttpPost]
        public async Task<IActionResult> PostCarrier([FromBody] Carrier carrier)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _carrierService.AddCarrierAsync(carrier);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Carrier object is incomplete or contains wrong data.");
            }
            

            return CreatedAtAction("GetCarrier", new {id = carrier.Id}, carrier);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrier([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carrierToDelete = await _carrierService.GetCarrierAsync(id);
            if (carrierToDelete == null)
                return NotFound();

            await _carrierService.RemoveCarrierAsync(carrierToDelete);

            return Ok();
        }

    }
}