using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.PluginApis;
using PluginCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IList<IActionDescriptorChangeProvider> _actionDescriptorChangeProviders;
        private readonly PluginFinder _pluginFinder;

        public TestController(IEnumerable<IActionDescriptorChangeProvider> actionDescriptorChangeProviders, PluginFinder pluginFinder)
        {
            _actionDescriptorChangeProviders = actionDescriptorChangeProviders.ToList();
            _pluginFinder = pluginFinder;
        }

        public ActionResult Get()
        {
            //var plugins = PluginFinder.EnablePlugins<BasePlugin>().ToList();
            var plugins2 = _pluginFinder.EnablePlugins<ITestPlugin>().ToList();

            foreach (var item in plugins2)
            {
                string words = item.Say();
                Console.WriteLine(words);
            }

            return Ok("");
        }
    }
}
