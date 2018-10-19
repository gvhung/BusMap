using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.Carriers;
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
        public async Task<IActionResult> GetAllCarriers()
            => await GetAllCarriersFunc(async () => await _carrierService.GetAllCarriersAsync());

        [HttpGet("routes")]
        public async Task<IActionResult> GetAllCarriersIncludeRoutes()
            => await GetAllCarriersFunc(async () => await _carrierService.GetAllCarriersIncludeRoutesAsync());

        [HttpGet("routesBusStops")]
        public async Task<IActionResult> GetAllCarriersIncludeRoutesBusStops()
            => await GetAllCarriersFunc(async () => await _carrierService.GetAllCarriersIncludeRoutesBusStopsAsync());

        public async Task<IActionResult> GetAllCarriersFunc(Func<Task<IEnumerable<CarriersCarrierDto>>> getAllCarriersFunc)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carriers = await getAllCarriersFunc();

            if (carriers == null || carriers.Count() < 1)
                return NotFound();

            return Ok(carriers);
        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCarrier([FromRoute] int id)
            => await GetCarrierFunc(id, async x => await _carrierService.GetCarrierAsync(id));

        [HttpGet("{id:int}/routes")]
        public async Task<IActionResult> GetCarrierIncludeRoutes([FromRoute] int id)
            => await GetCarrierFunc(id, async x => await _carrierService.GetCarrierIncludeRoutesAsync(id));

        [HttpGet("{id:int}/routesBusStops")]
        public async Task<IActionResult> GetCarrierIncludeRoutesBusStops([FromRoute] int id)
            => await GetCarrierFunc(id, async x => await _carrierService.GetCarrierIncludeRoutesBusStopsAsync(id));

        public async Task<IActionResult> GetCarrierFunc(int id, Func<int, Task<CarriersCarrierDto>> getCarrierFunc)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carrier = await getCarrierFunc(id);

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