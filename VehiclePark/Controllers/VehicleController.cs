using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.ComponentModel;
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
        [Description("Gets all the vehicles")]
        [EnableQuery]
        public async Task<ActionResult<List<Vehicle>>> GetAllVehiclesAsync()
        {
            var vehicles = await _vehicleService.GetAllVehicles();

            // if the request send back a list of vehicles, return OK
            if (vehicles != null)
            {
                return Ok(vehicles);
            }
            else
            {
                return BadRequest("No vehicles found");
            }
        }

        [HttpPost]
        [Description("Posts dummy vehicles")]
        public async Task<ActionResult> PostDummyVehicles()
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
        [Description("Updates a vehicle")]
        public async Task<ActionResult> UpdateVehicle(int id, [FromBody] Vehicle vehicle)
        {
            var request = await _vehicleService.UpdateVehicle(id, vehicle);

            if (request != null)
            {
                return Ok(request);
            }
            else
            {
                return BadRequest("Vehicle not found");
            }
        }

        [HttpGet("{id}")]
        [Description("Validates a license plate")]
        public async Task<ActionResult> ValidateLicensePlate(int id)
        {
            // sends the id to the service to validate the license plate
            var request = await _vehicleService.ValidateLicensePlate(id);

            // if the request returns data, then the license plate is valid
            if (request)
            {
                return Ok("License plate is valid");
            }
            else
            {
                return BadRequest("License plate is not valid");
            }
        }
    }
}
