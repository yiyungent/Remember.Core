using Framework.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.PluginApis
{
    public interface ITestPlugin : IPlugin
    {
        string Say();
    }
}
