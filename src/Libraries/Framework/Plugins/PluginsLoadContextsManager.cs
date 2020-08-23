using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Framework.Plugins
{
    /// <summary>
    /// 一个插件的所有dll由 一个 <see cref="CollectibleAssemblyLoadContext"/> 管理
    /// <see cref="PluginsLoadContexts"/> 记录管理了所有 插件的<see cref="CollectibleAssemblyLoadContext"/>
    /// <see cref="PluginsLoadContextsManager"/> 是对 <see cref="PluginsLoadContexts"/>的封装, 使其更好管理插件加载释放的行为
    /// </summary>
    public class PluginsLoadContextsManager
    {
        /// <summary>
        /// 加载插件程序集
        /// </summary>
        /// <param name="pluginId"></param>
        public static void LoadPlugin(string pluginId)
        {
            // 此插件的 加载上下文
            var context = new CollectibleAssemblyLoadContext();

            #region 加载插件主dll
            // 插件的主dll, 不包括插件项目引用的dll
            string pluginMainDllFilePath = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId, $"{pluginId}.dll");
            Assembly pluginMainAssembly;
            using (var fs = new FileStream(pluginMainDllFilePath, FileMode.Open))
            {
                // 使用此方法, 就不会导致dll被锁定
                pluginMainAssembly = context.LoadFromStream(fs);
            }
            #endregion

            // TODO:未测试 加载插件引用的dll: 方法二: 
            //AssemblyName[] referenceAssemblyNames = pluginMainAssembly.GetReferencedAssemblies();
            //foreach (var assemblyName in referenceAssemblyNames)
            //{
            //    context.LoadFromAssemblyName(assemblyName);
            //}

            // TODO: 跳过不需要加载的 dll, eg: ASP.NET Core Shared Framework, 主程序中已有dll

            #region 加载插件引用的dll
            // 加载插件引用的dll
            // eg: xxx/Plugins/HelloWorld
            string pluginDirPath = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId);
            var pluginDir = new DirectoryInfo(pluginDirPath);
            // 插件引用的所有dll (排除主dll)
            var allReferenceFileInfos = pluginDir.GetFiles("*.dll").Where(p => p.Name != $"{pluginId}.dll");
            foreach (FileInfo file in allReferenceFileInfos)
            {
                using (var sr = new StreamReader(file.OpenRead()))
                {
                    context.LoadFromStream(sr.BaseStream);
                }
            }
            #endregion

            // 这个插件加载上下文 放入 集合中
            PluginsLoadContexts.AddPluginContext(pluginId, context);
        }

        public static void UnloadPlugin(string pluginId)
        {
            PluginsLoadContexts.RemovePluginContext(pluginId);
        }
    }
}
