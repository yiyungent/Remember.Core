using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Framework.Plugins
{
    public class PluginsLoadContextsManager
    {
        /// <summary>
        /// 加载插件程序集
        /// </summary>
        /// <param name="pluginId"></param>
        public static void LoadPlugin(string pluginId)
        {
            var context = new CollectibleAssemblyLoadContext();
            // TODO: 插件的主dll, 不包括插件项目引用的dll
            string pluginMainDllFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins", pluginId, $"{pluginId}.dll");

            using (var fs = new FileStream(pluginMainDllFilePath, FileMode.Open))
            {
                // 使用此方法, 就不会导致dll被锁定
                var pluginAssembly = context.LoadFromStream(fs);

                PluginsLoadContexts.AddPluginContext(pluginId, context);
            }
        }

        public static void UnloadPlugin(string pluginId)
        {
            PluginsLoadContexts.RemovePluginContext(pluginId);
        }
    }
}
