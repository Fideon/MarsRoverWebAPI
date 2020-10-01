using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarsRoverWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RoverController : ControllerBase
    {
        // GET api/<RoverController>/1
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RoverController>
        [HttpPost]
        public RoverResponse Post([FromBody] RoverInstructions value)
        {
            // Retreive rover. If rover does not exist, then create a new one.
            Rover rover = SessionRoverExtension.GetObject<Rover>(HttpContext.Session, value.RoverId) == null ? new Rover(value.RoverId, "N", 0, 0) : SessionRoverExtension.GetObject<Rover>(HttpContext.Session, value.RoverId);

            // Execute all instructions sent for rover.
            rover = RoverOperationsService.ExecuteInstructions(value.MovementInstruction, rover);

            // Save rover, and return result.
            SessionRoverExtension.SetObject(HttpContext.Session, value.RoverId, rover);
            return new RoverResponse { Message = $"Rover {rover.RoverId} has moved. Orientation is {rover.RoverOrientation}.", CurrentPosition = $"({rover.RoverX},{rover.RoverY})" };
        }
    }
}
