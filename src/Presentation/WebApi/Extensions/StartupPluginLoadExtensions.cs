﻿using Framework.Plugins;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApi.Extensions
{
    /// <summary>
    /// 在程序启动时加载所有 已安装的插件
    /// </summary>
    public static class StartupPluginLoadExtensions
    {
        public static void AddPluginLoad(this IServiceCollection services)
        {
            // 获取PluginConfigModel
            #region 获取 获取PluginConfigModel
            PluginConfigModel pluginConfigModel = PluginConfigModelFactory.Create();
            #endregion

            // 已启用的插件
            #region 加载 已启用插件的Assemblies
            IList<string> enabledPluginIds = pluginConfigModel.EnabledPlugins;
            foreach (var pluginId in enabledPluginIds)
            {
                PluginsLoadContextsManager.LoadPlugin(pluginId);
            }
            #endregion

        }
    }
}
