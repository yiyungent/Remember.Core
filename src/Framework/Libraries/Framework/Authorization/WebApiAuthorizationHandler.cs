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

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       WebApiRequirement requirement)
        {
            var claim = context.User.Claims.FirstOrDefault(c => c.Type == "scopes");
            if (claim != null && int.TryParse(claim.Value, out int userId))
            {
                // TODO: 1. 获取此请求资源 的描述
                string authKey = string.Empty;


                // TODO: 2. 根据用户ID 查 是否拥有此资源权限
                bool isPass = true;

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

            return Task.CompletedTask;
        }
    }
}