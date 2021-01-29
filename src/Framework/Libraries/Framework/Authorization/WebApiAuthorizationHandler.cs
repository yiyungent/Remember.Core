using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Framework.Authorization
{
    public class WebApiAuthorizationHandler : AuthorizationHandler<WebApiRequirement>
    {
        public WebApiAuthorizationHandler()
        {
        }

        /// <summary>
        /// 必须在其中呼叫一次 <see cref="AuthorizationHandlerContext.Succeed(IAuthorizationRequirement)"/> 代表满足 <see cref="WebApiRequirement"/>，否则皆为 不满足此 Requirement
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       WebApiRequirement requirement)
        {
            Console.WriteLine($"Framework.Authorization.WebApiAuthorizationHandler.HandleRequirementAsync: before: context.HasFailed: {context.HasFailed}, context.HasSucceeded: {context.HasSucceeded}");

            var claims = context.User.Claims.ToList();

            var claim = context.User.Claims.FirstOrDefault(c => c.Type == "scope");

            Console.WriteLine($"Framework.Authorization.WebApiAuthorizationHandler.HandleRequirementAsync: claim.Value: {claim.Value}");

            #region 效验用户权限
            //if (claim != null && int.TryParse(claim.Value, out int userId))
            //{
            //    // TODO: 1. 获取此请求资源 的描述
            //    string authKey = string.Empty;


            //    // TODO: 2. 根据用户ID 查 是否拥有此资源权限
            //    bool isPass = true;

            //    if (isPass)
            //    {
            //        context.Succeed(requirement);
            //    }
            //    else
            //    {
            //        context.Fail();
            //    }
            //}
            //else
            //{
            //    context.Fail();
            //}
            #endregion

            context.Succeed(requirement);

            Console.WriteLine($"Framework.Authorization.WebApiAuthorizationHandler.HandleRequirementAsync: after: context.HasFailed: {context.HasFailed}, context.HasSucceeded: {context.HasSucceeded}");

            return Task.CompletedTask;
        }
    }
}