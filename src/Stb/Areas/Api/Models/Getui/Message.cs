using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api.Models.Getui
{
    public class Message
    {
        public string appkey { get; set; }

        public bool is_offline { get; set; } = true;

        public int offline_expire_time { get; set; } = 1000 * 3600 * 12;

        //public int push_network_type { get; set; } = 0;

        public string msgtype { get; set; } = "transmission";
    }
}
