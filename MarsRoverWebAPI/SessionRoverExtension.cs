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
        public static void SetObject(this ISession session, string key, Rover rover)
        {
            session.SetString(key, JsonConvert.SerializeObject(rover));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
