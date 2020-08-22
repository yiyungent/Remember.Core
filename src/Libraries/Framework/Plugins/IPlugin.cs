using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Plugins
{
    public interface IPlugin
    {
        (bool IsSuccess, string Message) Install();

        (bool IsSuccess, string Message) Uninstall();

        (bool IsSuccess, string Message) Update(string currentVersion, string targetVersion);

    }
}
