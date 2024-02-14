using Microsoft.AspNetCore.Mvc;

namespace VehiclePark.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllVehicles()
        {
            return Ok("This works");
        }
    }
}
