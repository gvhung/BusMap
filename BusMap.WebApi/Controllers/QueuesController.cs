using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusMap.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueuesController : ControllerBase
    {
        private readonly IQueueService _service;

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

    }
}