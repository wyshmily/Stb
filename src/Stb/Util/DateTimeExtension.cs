using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Util
{
    public static class DateTimeExtension
    {
        public static long ToUnixSeconds(this DateTime date) => new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds();

        public static long ToUnixMilliSeconds(this DateTime date) => new DateTimeOffset(date).ToUniversalTime().ToUnixTimeMilliseconds();
    }
}
