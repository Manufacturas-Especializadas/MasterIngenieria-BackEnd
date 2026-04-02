using Application.Dtos;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly MasterService _masterService;

        public MetricsController(MasterService masterService) => _masterService = masterService;

        [HttpGet]
        [Route("top-cycle-times/{line}")]
        public async Task<ActionResult<TopCycleTimeDto>> GetTopByLine(int line)
        {
            var result = await _masterService.GetTopFiveByLine(line);

            if (!result.Any()) return NotFound("No se encontrarón registros para esta línea");

            return Ok(result);
        }

        [HttpGet]
        [Route("lines")]
        public async Task<ActionResult<IEnumerable<int>>> GetLines()
        {
            var lines = await _masterService.GetLines();

            return Ok(lines);
        }
    }
}