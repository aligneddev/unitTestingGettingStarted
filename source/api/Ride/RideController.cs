using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Ride
{
    [Route("api/[controller]")]
    public class RideController : Controller
    {
        public RideController(IRideCommand )
        {

        }
        [HttpPost("[action]")]
        public Task<ActionResult> Create([FromBody] RideRequest rideRequest)
        {
            var 
            return Ok();
        }
    }
}
