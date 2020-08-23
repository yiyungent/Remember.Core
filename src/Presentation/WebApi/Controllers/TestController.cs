using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.PluginApis;
using Framework.Plugins;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public ActionResult Get()
        {
            
            var plugins = PluginFinder.EnablePlugins<BasePlugin>().ToList();
            var plugins2 = PluginFinder.EnablePlugins<ITestPlugin>().ToList();

            foreach (var item in plugins2)
            {
                string words = item.Say();
                Console.WriteLine(words);
            }

            return Ok("");
        }
    }
}
