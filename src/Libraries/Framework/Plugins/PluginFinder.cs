using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Framework.Plugins
{
    public class PluginFinder
    {
        /// <summary>
        /// 实现了指定接口或类型 的启用插件
        /// </summary>
        /// <typeparam name="TPlugin">可以是一个接口，一个抽象类，一个普通实现类, 只要实现了 <see cref="IPlugin"/>即可</typeparam>
        /// <returns></returns>
        public static IEnumerable<TPlugin> EnablePlugins<TPlugin>()
            where TPlugin : IPlugin // BasePlugin
        {
            // TODO: 目前这里还有问题, 不应该写为 BasePlugin, 不利于扩展, 不利于插件开发者自己实现 Install , Uninstall等

            // 1.所有启用的插件 PluginId
            var pluginConfigModel = PluginConfigModelFactory.Create();
            IList<string> enablePluginIds = pluginConfigModel.EnabledPlugins;
            foreach (var pluginId in enablePluginIds)
            {
                if (PluginsLoadContexts.Any(pluginId))
                {
                    // 2.找到插件对应的Context
                    var context = PluginsLoadContexts.Get(pluginId);
                    // 3.找插件 主 Assembly
                    // Assembly.FullName: HelloWorld, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
                    Assembly pluginMainAssembly = context.Assemblies.Where(m => m.FullName.StartsWith($"{pluginId}, Version=")).FirstOrDefault();
                    if (pluginMainAssembly == null)
                    {
                        continue;
                    }
                    // 4.从插件主Assembly中 找实现了 TPlugin 接口的 Type, 若有多个，只要一个
                    Type pluginType = pluginMainAssembly.ExportedTypes.Where(m =>
                        (m.BaseType == typeof(TPlugin) || m.GetInterfaces().Contains(typeof(TPlugin)))
                        &&
                        !m.IsInterface
                        &&
                        !m.IsAbstract
                    ).FirstOrDefault();
                    if (pluginType == null)
                    {
                        continue;
                    }
                    // 5.实例化插件 Type
                    //(TPlugin)Activator.CreateInstance(pluginType,);
                    //try to resolve plugin as unregistered service
                    object instance = EngineContext.Current.ResolveUnregistered(pluginType);
                    //try to get typed instance
                    TPlugin typedInstance = (TPlugin)instance;
                    if (typedInstance == null)
                    {
                        continue;
                    }

                    yield return typedInstance;
                }
            }

        }
    }
}
