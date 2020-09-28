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
                //if (adminUserIds.Contains(userId))
                //{
                //    context.Succeed(requirement);
                //}
            }

            return Task.CompletedTask;
        }
    }
}