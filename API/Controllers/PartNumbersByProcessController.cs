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
        public async Task<IActionResult> GetStats()
        {
            var stats = await _service.GetParentPartNumbersStatsAsync();

            return Ok(stats);
        }

    }
}