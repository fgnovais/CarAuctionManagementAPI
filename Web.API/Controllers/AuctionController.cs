using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController, Produces("application/json")]
    [Route("api/[controller]")]
    public class AuctionController : Controller
    {
        private readonly ILogger<AuctionController> _logger;
        private readonly IAuctionService _service;
        public AuctionController(ILogger<AuctionController> logger, IAuctionService service)
        {
            _logger = logger;
            _service = service;
        } // inject.

        [HttpPatch("start")]
        public async Task<ActionResult<AuctionDTO>> StartAuction([FromQuery] Guid vehicleId)
        {
            var result = await _service.StartAuction(vehicleId);
            if (result.IsSuccess)
            {
                return Ok(result._value);
            }
            else
            {
                return BadRequest(result._error);
            }
        }

        [HttpPatch("close")]
        public async Task<ActionResult<AuctionDTO>> CloseAuction([FromQuery] Guid vehicleId)
        {
            var result = await _service.CloseAuction(vehicleId);
            if (result.IsSuccess)
            {
                return Ok(result._value);
            }
            else
            {
                return BadRequest(result._error);
            }
        }

        [HttpPatch("bid")]
        public async Task<ActionResult<AuctionDTO>> PlaceBid([FromQuery] PlaceBidDTO request)
        {
            var result = await _service.PlaceBidOnAuction(request.VehicleId, request.Bid);
            if (result.IsSuccess)
            {
                return Ok(result._value);
            }
            else
            {
                return BadRequest(result._error);
            }
        }
    }
}
