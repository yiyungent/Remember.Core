using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

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

        public ActionResult<UserInfo> Get(int id)
        {
            UserInfo viewModel = _userInfoService.Find(m => m.ID == id && !m.IsDeleted);


            return Ok(viewModel);
        }

        #endregion
    }
}
