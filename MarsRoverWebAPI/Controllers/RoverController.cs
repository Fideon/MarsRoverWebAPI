using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        public RoverResponse Get(string id)
        {
            // Check JSON values are within constraints
            if (id.Length > 10)
            {
                return new RoverResponse { Message = "Rover ID is over the allowed limit. Rover ID should be less than 10 characters.", CurrentPosition = "()" };
            }

            Rover rover = SessionRoverExtension.GetRover<Rover>(HttpContext.Session, id);

            if (rover == null)
                return new RoverResponse { Message = "Rover does not exist.", CurrentPosition = "()" };

            return new RoverResponse { Message = $"Rover {rover.RoverId}'s orientation is: {rover.RoverOrientation}", CurrentPosition = $"({rover.RoverX},{rover.RoverY})" };
        }

        // POST api/<RoverController>
        [HttpPost]
        public RoverResponse Post([FromBody] RoverInstructions value)
        {
            // REGular EXpression for checking instructions are limited to R, M, and L characters.
            Regex instructionsRegex = new Regex(@"^[RML]+$");

            // Check JSON values are within constraints
            if (value.RoverId.Length > 10 || value.MovementInstruction.Length > 100)
            {
                return new RoverResponse { Message = "Rover ID and/or Movement Instructions are over the allowed limit. Rover ID should be less than 10 characters. Instructions should be less than 100 characters.", CurrentPosition = "()" };
            }
            else if (!instructionsRegex.IsMatch(value.MovementInstruction))
            {
                return new RoverResponse { Message = "Movement Instructions are incorrect. Instructions can only have values of 'R', 'L', and 'M'.", CurrentPosition = "()" };
            }

            // Retreive rover from Session Storage. If rover does not exist, then create a new one.
            Rover rover = SessionRoverExtension.GetRover<Rover>(HttpContext.Session, value.RoverId) == null ? new Rover(value.RoverId, "N", 0, 0) : SessionRoverExtension.GetRover<Rover>(HttpContext.Session, value.RoverId);

            // Execute all instructions sent for rover.
            rover = RoverOperationsService.ExecuteInstructions(value.MovementInstruction, rover);

            // Save rover to Session Storage, and return result.
            SessionRoverExtension.SetRover(HttpContext.Session, value.RoverId, rover);
            return new RoverResponse { Message = $"Rover {rover.RoverId} has moved. Orientation is: {rover.RoverOrientation}.", CurrentPosition = $"({rover.RoverX},{rover.RoverY})" };
        }
    }
}
