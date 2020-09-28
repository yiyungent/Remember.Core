using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Framework.Authorization
{
    public class WebApiAuthorizeAttribute : AuthorizeAttribute
    {
        public WebApiAuthorizeAttribute() : base("webapi")
        {

        }
    }
}
