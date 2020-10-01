using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverWebAPI
{
    public static class SessionRoverExtension
    {
        public static void SetRover(this ISession session, string key, Rover rover)
        {
            session.SetString(key, JsonConvert.SerializeObject(rover));
        }

        public static Rover GetRover<Rover>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<Rover>(value);
        }
    }
}
