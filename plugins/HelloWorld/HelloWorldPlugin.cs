using Framework.PluginApis;
using Framework.Plugins;
using System;

namespace HelloWorld
{
    public class HelloWorldPlugin : BasePlugin, ITestPlugin
    {
        public string Say()
        {
            return "Hello World!";
        }
    }
}
