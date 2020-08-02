using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Common
{
    public class ResponseData
    {
        public int code { get; set; }

        public string message { get; set; }

        public object data { get; set; }
    }
}
