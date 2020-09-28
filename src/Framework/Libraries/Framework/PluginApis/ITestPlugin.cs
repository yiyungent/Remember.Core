using System;
using System.Collections.Generic;
using System.Text;
using PluginCore;

namespace Framework.PluginApis
{
    public interface ITestPlugin : IPlugin
    {
        string Say();
    }
}
