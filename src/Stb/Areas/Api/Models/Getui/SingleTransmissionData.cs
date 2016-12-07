using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api.Models.Getui
{
    public class SingleTransmissionData
    {
        public Message message { get; set; }

        public Transmission transmission { get; set; }

        public string cid { get; set; }

        public string requestid {get;set;}
    }
}
