using System;
using System.Collections.Generic;
using System.Text;

namespace WebInstaller.Models
{
    public class UserModel
    {
        public _Id _id { get; set; }
        public string name { get; set; }
        public string uId { get; set; }
        public float followCnt { get; set; }
        public float fansCnt { get; set; }
        public float weiboCnt { get; set; }
        public string addr { get; set; }
        public string sex { get; set; }
        public string info { get; set; }
        public string from { get; set; }
    }

    public class _Id
    {
        public string oid { get; set; }
    }

}
