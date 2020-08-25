using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using WebApi.Models.Common;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Fields

        private readonly IUserInfoService _userInfoService;
        #endregion

        #region Ctor

        public UserController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }
        #endregion

        #region Actions

        [HttpGet]
        public async Task<ActionResult<UserInfo>> Get(int id)
        {
            UserInfo viewModel = await _userInfoService.FindAsync(m => m.ID == id && !m.IsDeleted);

            return Ok(viewModel);
        }

        [HttpGet(nameof(Info))]
        public async Task<ActionResult<ResponseModel>> Info()
        {
            ResponseModel responseData = new ResponseModel();
            responseData.code = 1;
            responseData.message = "获取成功";
            responseData.data = new { name = "admin", avatar = "" };

            return await Task.FromResult(responseData);
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult<ResponseModel>> Login()
        {
            ResponseModel responseData = new ResponseModel();
            responseData.code = 1;
            responseData.message = "登陆成功";
            responseData.data = new { token = "efrwnnrwt" };

            return await Task.FromResult(responseData);
        }

        #endregion
    }
}
