using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Framework.Plugins
{
    public class PluginInfoModelFactory
    {
        #region 即时读取指定 plugin info.json
        public static PluginInfoModelWrapper Create(string pluginId)
        {
            PluginInfoModelWrapper pluginInfoModel = new PluginInfoModelWrapper();
            string pluginDir = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId);
            string pluginInfoFilePath = Path.Combine(pluginDir, "info.json");

            if (!File.Exists(pluginInfoFilePath))
            {
                return null;
            }
            try
            {
                string pluginInfoJsonStr = File.ReadAllText(pluginInfoFilePath, Encoding.UTF8);
                JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
                jsonSerializerOptions.PropertyNameCaseInsensitive = true;
                pluginInfoModel = JsonSerializer.Deserialize<PluginInfoModelWrapper>(pluginInfoJsonStr, jsonSerializerOptions);
                pluginInfoModel.PluginId = PluginPathProvider.GetPluginFolderNameByDir(pluginDir);
            }
            catch (Exception ex)
            {
                pluginInfoModel = null;
            }

            return pluginInfoModel;
        }
        #endregion

        #region 即时读取插件目录下所有 plugin info.json
        public static IList<PluginInfoModelWrapper> CreateAll()
        {
            IList<PluginInfoModelWrapper> pluginInfoModels = new List<PluginInfoModelWrapper>();
            IList<string> pluginDirs = PluginPathProvider.AllPluginDir();
            foreach (var dir in pluginDirs)
            {
                // 从 dir 中解析出 pluginId
                // 约定: 插件文件夹名=PluginID=插件主.dll
                string pluginId = PluginPathProvider.GetPluginFolderNameByDir(dir);
                PluginInfoModelWrapper model = Create(pluginId);
                pluginInfoModels.Add(model);
            }
            // 去除为 null: 目标插件信息不存在，或者格式错误的
            pluginInfoModels = pluginInfoModels.Where(m => m != null).ToList();

            return pluginInfoModels;
        }
        #endregion
    }

    public class PluginInfoModelWrapper : PluginInfoModel
    {
        public string PluginId { get; set; }

        /// <summary>
        /// 插件状态
        /// </summary>
        public PluginStatus Status { get; set; }
    }

    public enum PluginStatus
    {
        Enabled = 0,
        Disabled = 1,
        Uninstalled = 2
    }
}
