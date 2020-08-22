using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Plugins
{
    /// <summary>
    /// https://www.cnblogs.com/lwqlun/p/11395828.html
    /// 1.当加载插件的时候，我们需要将当前插件的程序集加载上下文放到_pluginContexts字典中。字典的key是插件的名称，字典的value是插件的程序集加载上下文。
    /// 2.当移除一个插件的时候，我们需要使用Unload方法，来释放当前的程序集加载上下文。
    /// </summary>
    public static class PluginsLoadContexts
    {
        #region Fields

        private static Dictionary<string, CollectibleAssemblyLoadContext>
            _pluginContexts; 

        #endregion

        #region Ctor
        static PluginsLoadContexts()
        {
            _pluginContexts = new Dictionary<string, CollectibleAssemblyLoadContext>();
        } 
        #endregion

        #region Methods
        public static bool Any(string pluginId)
        {
            return _pluginContexts.ContainsKey(pluginId);
        }

        public static void RemovePluginContext(string pluginId)
        {
            if (_pluginContexts.ContainsKey(pluginId))
            {
                _pluginContexts[pluginId].Unload();
                _pluginContexts.Remove(pluginId);
            }
        }

        public static CollectibleAssemblyLoadContext GetContext(string pluginId)
        {
            return _pluginContexts[pluginId];
        }

        public static void AddPluginContext(string pluginId,
             CollectibleAssemblyLoadContext context)
        {
            _pluginContexts.Add(pluginId, context);
        } 
        #endregion

    }
}
