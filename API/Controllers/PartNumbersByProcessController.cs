using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartNumbersByProcessController : ControllerBase
    {
        private readonly IPartNumberService _service;

        public PartNumbersByProcessController(IPartNumberService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Dashboard-parentPartNumbers")]
        public async Task<IActionResult> GetStats(
            [FromQuery] string? parentPartNumber,
            [FromQuery] string? childPartNumber,
            [FromQuery] string? process
            )
        {
            var stats = await _service.GetParentPartNumbersStatsAsync(
                parentPartNumber, 
                childPartNumber, 
                process
            );

            return Ok(stats);
        }


        [HttpGet]
        [Route("Dashboard-childPartNumbers")]
        public async Task<IActionResult> GetChildPartNumbersStats(
            [FromQuery] string? parentPartNumber,
            [FromQuery] string? childPartNumber,
            [FromQuery] string? process
            )
        {
            var stats = await _service.GetChildPartNumbersStatsAsync(
                parentPartNumber,
                childPartNumber,
                process
            );

            return Ok(stats);
        }

        [HttpGet]
        [Route("Kpi-stats")]
        public async Task<IActionResult> GetKpiStats()
        {
            var kpiStats = await _service.GetKpiStatsAsync();

            return Ok(kpiStats);
        }
    }
}