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

        public override (bool IsSuccess, string Message) AfterEnable()
        {
            Console.WriteLine($"{nameof(HelloWorldPlugin)}: {nameof(AfterEnable)}");
            return base.AfterEnable();
        }

        public override (bool IsSuccess, string Message) BeforeDisable()
        {
            Console.WriteLine($"{nameof(HelloWorldPlugin)}: {nameof(BeforeDisable)}");
            return base.BeforeDisable();
        }
    }
}
