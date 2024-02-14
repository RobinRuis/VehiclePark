using Microsoft.AspNetCore.Mvc;
using VehiclePark.Models;
using VehiclePark.Services;

namespace VehiclePark.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public IActionResult GetAllVehicles()
        {
            return Ok("This works");
        }

        [HttpPost]
        public async Task<IActionResult> PostDummyVehicles()
        {
            var vehicles = await _vehicleService.PostDummyVehicles();

            // if the request send back a list of vehicles, return OK
            // if the request send back an empty list, the db already got populated with vehicles
            if (vehicles.Count > 0)
            {
                return Ok(vehicles);
            }
            else
            {
                return BadRequest("Vehicles already in database");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVehicle(int id, Vehicle vehicle)
        {
            var request = _vehicleService.UpdateVehicle(id, vehicle);

            if (request != null)
            {
                return Ok(request);
            }
            else
            {
                return BadRequest("Vehicle not found");
            }
        }   
    }
}
