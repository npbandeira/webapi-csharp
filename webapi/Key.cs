using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi
{
    public class Key
    {
        public static string Secret = Environment.GetEnvironmentVariable("SECRET");
    }
}