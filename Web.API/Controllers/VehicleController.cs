using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.DTO;
using Domain.Entities.Vehicles;

namespace Web.API.Controllers
{
    [ApiController, Produces("application/json")]
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly IVehicleService _service;
        public VehicleController(ILogger<VehicleController> logger, IVehicleService service)
        {
            _logger = logger;
            _service = service;
        } // inject.

        [HttpGet()]
        public async Task<ActionResult<List<Vehicle>>> GetVehiclesAsync([FromQuery] VehicleType? type, [FromQuery] string? manufacturer, [FromQuery] string? model, [FromQuery] int? year) // show all vehicles if no parameters are sent
        {
            VehicleFilter filter = new VehicleFilter(type, manufacturer, model, year);

           // IList<VehicleDTO> filteredVehicles = await _service.GetFilteredVehiclesAsync(filter);
            var result = await _service.GetFilteredVehicles(filter);
            if (result.IsSuccess)
            {
                return Ok(result._value);
            }
            else
            {
                return BadRequest(result._error);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<Vehicle>> CreateVehicle([FromBody] CreateVehicleRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            Vehicle vehicle = (request) switch
            {
                CreateSuvDTO suv => new SUV(suv.Manufacturer, suv.Model, suv.StartingBid, suv.Year, suv.NumberOfSeats),
                CreateTruckDTO truck => new Truck(truck.Manufacturer, truck.Model, truck.StartingBid, truck.Year, truck.LoadCapacity),
                CreateSedanDTO sedan => new Sedan(sedan.Manufacturer, sedan.Model, sedan.StartingBid, sedan.Year, sedan.NumberOfDoors),
                CreateHatchbackDTO hatchback => new Hatchback(hatchback.Manufacturer, hatchback.Model, hatchback.StartingBid, hatchback.Year, hatchback.NumberOfDoors),
                _ => throw new NotImplementedException() // type not found
            };

            var result = await _service.AddVehicle(vehicle);

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
