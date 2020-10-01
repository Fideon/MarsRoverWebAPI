using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverWebAPI
{
    public class RoverInstructions
    {
        [JsonProperty("RoverId")]
        public string RoverId { get; set; }

        [JsonProperty("MovementInstruction")]
        public string MovementInstruction { get; set; }
    }
}
