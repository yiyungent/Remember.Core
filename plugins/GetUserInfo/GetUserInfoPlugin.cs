using Domain.Entities;
using Framework.PluginApis;
using Framework.Plugins;
using Services.Interface;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace GetUserInfo
{
    public class GetUserInfoPlugin : BasePlugin, ITestPlugin
    {
        private readonly IUserInfoService _userInfoService;

        public GetUserInfoPlugin(IUserInfoService userInfoService)
        {
            this._userInfoService = userInfoService;
        }

        public string Say()
        {
            UserInfo userInfo = _userInfoService.FirstOrDefaultAsync(m => !m.IsDeleted).Result;
            string rtn = $"用户名: {userInfo.UserName}, 创建时间: {userInfo.CreateTime.ToString()}";

            return rtn;
        }

        public override (bool IsSuccess, string Message) AfterEnable()
        {
            Console.WriteLine($"{nameof(GetUserInfoPlugin)}: {nameof(AfterEnable)}");
            return base.AfterEnable();
        }

        public override (bool IsSuccess, string Message) BeforeDisable()
        {
            Console.WriteLine($"{nameof(GetUserInfoPlugin)}: {nameof(BeforeDisable)}");
            return base.BeforeDisable();
        }
    }
}
