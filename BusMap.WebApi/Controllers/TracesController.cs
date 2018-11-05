using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusMap.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracesController : ControllerBase
    {
        private readonly ITraceService _traceService;

        public TracesController(ITraceService service)
        {
            _traceService = service;
        }

        [HttpPost("busStop")]
        public async Task<IActionResult> PostBusStopTrace([FromBody] BusStopTrace busStopTrace)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _traceService.AddBusStopTraceAsync(busStopTrace);
            }
            catch (DbUpdateException)
            {
                return BadRequest("please specify busStopId and hour");
            }

            return StatusCode(201);
        }
    }
}