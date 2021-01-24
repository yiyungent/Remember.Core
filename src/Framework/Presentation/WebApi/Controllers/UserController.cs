using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using WebApi.Models.Common;
using WebApi.Models.User;

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
            UserInfo viewModel = await _userInfoService.FindAsync(m => m.ID == id);

            return Ok(viewModel);
        }

        #region 用户基本信息
        [HttpGet(nameof(Info))]
        public async Task<ActionResult<ResponseModel>> Info()
        {
            ResponseModel responseData = new ResponseModel();




            responseData.code = 1;
            responseData.message = "获取成功";
            responseData.data = new { name = "admin", avatar = "" };

            return await Task.FromResult(responseData);
        } 
        #endregion

        #region 登录
        [HttpPost(nameof(Login))]
        public async Task<ActionResult<ResponseModel>> Login(LoginInputModel inputModel)
        {
            ResponseModel responseData = new ResponseModel();
            // TODO: IdentityServer4
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                responseData.code = -1;
                responseData.message = "登陆失败";
                return await Task.FromResult(responseData);
            }
            // request token
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "Remember.Core",
                ClientSecret = "Remember.Core Secret",
                Scope = "openid profile",
                GrantType = IdentityModel.OidcConstants.GrantTypes.Password,
                // TODO: 目前仅支持用户名登陆
                UserName = inputModel.Account,
                Password = inputModel.Password
            });
            if (tokenResponse.IsError)
            {
                Console.WriteLine(disco.Error);
                responseData.code = -1;
                responseData.message = "用户名或密码错误";
                return await Task.FromResult(responseData);
            }
            string accessToken = tokenResponse.AccessToken;


            responseData.code = 1;
            responseData.message = "登陆成功";
            responseData.data = new LoginResponseModel
            {
                Token = accessToken
            };

            return await Task.FromResult(responseData);
        } 
        #endregion

        #endregion
    }
}
