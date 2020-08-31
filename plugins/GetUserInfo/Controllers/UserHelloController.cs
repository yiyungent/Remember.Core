using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Framework.PluginApis;
using PluginCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Services.Interface;

namespace WebApi.Controllers
{
    [Route("api/plugins/[controller]")]
    [ApiController]
    public class UserHelloController : ControllerBase
    {
        private readonly IUserInfoService _userInfoService;

        public UserHelloController(IUserInfoService userInfoService)
        {
            this._userInfoService = userInfoService;
        }

        public ActionResult Get()
        {
            UserInfo userInfo = _userInfoService.FirstOrDefaultAsync(m => !m.IsDeleted).Result;
            string rtn = $"用户名: {userInfo.UserName}, 创建时间: {userInfo.CreateTime.ToString()}";

            return Ok(rtn);
        }
    }
}
