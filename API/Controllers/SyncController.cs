using Application.Interfaces;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly ISyncService _syncService;

        public SyncController(ISyncService syncService)
        {
            _syncService = syncService;
        }

        [HttpPost]
        [Route("master-industrial")]
        [ProducesResponseType(typeof(SyncResult), 200)]
        [ProducesResponseType(typeof(SyncResult), 500)]
        public async Task<IActionResult> SyncMasterData()
        {
            var result = await _syncService.SyncMasterFromExcelAsync();

            if (!result.Success)
            {
                return StatusCode(500, result);
            }

            return Ok(result);
        }
    }
}