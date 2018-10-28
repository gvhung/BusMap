using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.Queues;
using BusMap.WebApi.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusMap.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueuesController : ControllerBase
    {
        private readonly IQueueService _service;
        private readonly IMapper _mapper;

        public QueuesController(IQueueService service)
        {
            _service = service;
        }

        [HttpGet("routes")]
        public async Task<IActionResult> GetQueuedRoutes()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var queuedRoutes = await _service.GetRoutesQueueAsync();

            if (queuedRoutes == null || queuedRoutes.ToList().Count < 1)
                return NotFound();

            return Ok(queuedRoutes);
        }

        [HttpGet("routes/count")]
        public async Task<IActionResult> GetNumberOfQueuedRoutes()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var nOfQueuedROutes = await _service.GetNumberOfQueuedRoutesAsync();

            return Ok(nOfQueuedROutes);
        }

        [HttpGet("carriers/{id:int}")]
        public async Task<IActionResult> GetQueuedCarrier(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carrier = await _service.GetCarrierQueued(id);

            if (carrier == null)
                return NotFound();

            return Ok(carrier);
        }


        [HttpPost("routes")]
        public async Task<IActionResult> PostRouteToQueue([FromBody] RouteQueued routeQueued)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.AddRouteToQueueAsync(routeQueued);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Route queued object is incomplete or contains wrong data.");
            }

            return StatusCode(201);
        }

        [HttpPost("carriers")]
        public async Task<IActionResult> PostCarrierToQueue([FromBody] CarrierQueued carrierQueued)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _service.AddCarrierToQueueAsync(carrierQueued);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Carrier object is incomplete or contains wrong data.");
            }

            return CreatedAtAction("GetQueuedCarrier", new {id = carrierQueued.Id}, carrierQueued);
        }

        [HttpPut("routes/{id:int}")]
        public async Task<IActionResult> PutRoute([FromRoute] int id, [FromBody] RouteQueued routeQueued)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.UpdateRouteAsync(id, routeQueued);
            }
            catch(Exception)
            {
                return BadRequest("Wrong body data");
            }

            return StatusCode(202);
        }



    }
}