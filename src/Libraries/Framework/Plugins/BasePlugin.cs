using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Framework.Plugins
{
    public abstract class BasePlugin : IPlugin
    {
        public virtual (bool IsSuccess, string Message) Install()
        {
            return (true, "安装成功");
        }

        public virtual (bool IsSuccess, string Message) Uninstall()
        {
            return (true, "卸载成功");
        }

        public (bool IsSuccess, string Message) Update(string currentVersion, string targetVersion)
        {
            return (true, "更新成功");
        }
    }
}
