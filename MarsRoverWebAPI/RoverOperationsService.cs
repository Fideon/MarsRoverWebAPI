using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverWebAPI
{
    public static class RoverOperationsService
    {
        public static Rover ExecuteInstructions(string instructions, Rover rover)
        {
            foreach (char move in instructions.ToCharArray())
            {
                switch (move)
                {
                    case 'M':
                        rover = MoveRover(rover);
                        break;

                    case 'R':
                        rover.RoverOrientation = RotateRight(rover.RoverOrientation);
                        break;

                    case 'L':
                        rover.RoverOrientation = RotateLeft(rover.RoverOrientation);
                        break;
                }
            }
            return rover;
        }
        private static Rover MoveRover(Rover rover)
        {
            switch (rover.RoverOrientation)
            {
                case "N":
                    rover.RoverY++;
                    break;

                case "E":
                    rover.RoverX++;
                    break;

                case "S":
                    rover.RoverY--;
                    break;

                case "W":
                    rover.RoverX--;
                    break;

                default:
                    break;
            }
            return rover;
        }
        private static string RotateRight(string currentOrientation)
        {
            switch (currentOrientation)
            {
                case "N":
                    return "E";

                case "E":
                    return "S";

                case "S":
                    return "W";

                case "W":
                    return "N";

                default:
                    return "N";
            }
        }

        private static string RotateLeft(string currentOrientation)
        {
            switch (currentOrientation)
            {
                case "N":
                    return "W";

                case "W":
                    return "S";

                case "S":
                    return "E";

                case "E":
                    return "N";

                default:
                    return "N";
            }
        }
    }
}
