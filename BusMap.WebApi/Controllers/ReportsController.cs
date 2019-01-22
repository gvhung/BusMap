using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusMap.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
        }


        [HttpGet("routes/{routeId:int}")]
        public async Task<IActionResult> GetReportsForRoute(int routeId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reports = await _service.GetRouteReportsForRouteAsync(routeId);

            if (reports == null || !reports.Any())
                return NotFound();

            return Ok(reports);
        }

        [HttpPost("routes")]
        public async Task<IActionResult> PostRouteReport(RouteReport routeReport)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddRouteReportAsync(routeReport);

            return StatusCode(StatusCodes.Status201Created);
        }



        [HttpGet("delay/{routeId:int}")]
        public async Task<IActionResult> GetLatestDelaysForRoute(int routeId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var routeDelays = await _service.GetLatestRouteDelaysAsync(routeId);

            if (routeDelays == null)
                return NotFound("Not found any delays for route");

            return Ok(routeDelays);
        }

        [HttpPost("delay")]
        public async Task<IActionResult> PostRouteDelay(RouteDelay routeDelay)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.AddRouteDelayAsync(routeDelay);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }

            return StatusCode(StatusCodes.Status201Created);
        }


    }
}