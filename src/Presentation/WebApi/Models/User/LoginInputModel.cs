using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.User
{
    public class LoginInputModel
    {
        /// <summary>
        /// 用户名/邮箱/手机号
        /// </summary>
        public string Account { get; set; }

        public string Password { get; set; }
    }
}
