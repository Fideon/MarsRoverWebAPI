using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverWebAPI
{
    public class Rover
    {
        public string RoverId;
        public string RoverOrientation;
        public int RoverX;
        public int RoverY;

        public Rover(string roverId, string roverOrientation, int roverX, int roverY)
        {
            this.RoverId = roverId;
            this.RoverOrientation = roverOrientation;
            this.RoverX = roverX;
            this.RoverY = roverY;
        }
    }
}
