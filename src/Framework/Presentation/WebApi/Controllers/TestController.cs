using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.PluginApis;
using PluginCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Core.Configuration;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IList<IActionDescriptorChangeProvider> _actionDescriptorChangeProviders;
        private readonly PluginFinder _pluginFinder;
        private readonly RemConfig _remConfig;

        public TestController(IEnumerable<IActionDescriptorChangeProvider> actionDescriptorChangeProviders, PluginFinder pluginFinder, IOptionsMonitor<RemConfig> optionsMonitor)
        {
            _actionDescriptorChangeProviders = actionDescriptorChangeProviders.ToList();
            _pluginFinder = pluginFinder;
            _remConfig = optionsMonitor.CurrentValue;
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
