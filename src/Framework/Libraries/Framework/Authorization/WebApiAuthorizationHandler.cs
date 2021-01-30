using System;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Services.Interface;

namespace Framework.Authorization
{
    public class WebApiAuthorizationHandler : AuthorizationHandler<WebApiRequirement>
    {
        private readonly IUserInfoService _userInfoService;

        public WebApiAuthorizationHandler(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        /// <summary>
        /// 必须在其中呼叫一次 <see cref="AuthorizationHandlerContext.Succeed(IAuthorizationRequirement)"/> 代表满足 <see cref="WebApiRequirement"/>，否则皆为 不满足此 Requirement
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       WebApiRequirement requirement)
        {
            //Console.WriteLine($"Framework.Authorization.WebApiAuthorizationHandler.HandleRequirementAsync: before: context.HasFailed: {context.HasFailed}, context.HasSucceeded: {context.HasSucceeded}");

            var claims = context.User.Claims.ToList();
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            #region 获取权限键
            string authKey = string.Empty;
            // 参考: https://docs.microsoft.com/zh-cn/aspnet/core/security/authorization/policies?view=aspnetcore-5.0#access-mvc-request-context-in-handlers
            if (context.Resource is Endpoint endpoint)
            {
                var actionDescriptor = endpoint.Metadata.GetMetadata<Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor>();
                // api/admin/Plugins/List
                string routeTemplate = actionDescriptor.AttributeRouteInfo.Template;
                routeTemplate = routeTemplate.ToLower();
                string[] routeKeys = routeTemplate.Split("/", StringSplitOptions.RemoveEmptyEntries);
                authKey = string.Join(".", routeKeys);
            }
            #endregion


            #region 效验用户权限
            if (string.IsNullOrEmpty(authKey))
            {
                context.Fail();
                await Task.CompletedTask;
            }
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                // 1. 根据用户ID 查 是否拥有此资源权限
                bool isPass = false;
                try
                {
                    isPass = await this._userInfoService.HasAuth(userId, authKey);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                if (isPass)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            else
            {
                context.Fail();
            }
            #endregion

            //Console.WriteLine($"Framework.Authorization.WebApiAuthorizationHandler.HandleRequirementAsync: after: context.HasFailed: {context.HasFailed}, context.HasSucceeded: {context.HasSucceeded}");

            await Task.CompletedTask;
        }
    }
}