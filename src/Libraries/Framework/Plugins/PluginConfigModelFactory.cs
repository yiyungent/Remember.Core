﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Framework.Plugins
{
    public class PluginConfigModelFactory
    {
        public static PluginConfigModel Create()
        {
            PluginConfigModel pluginConfigModel = new PluginConfigModel();
            string pluginConfigFilePath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "plugin.config.json");
            string pluginConfigJsonStr = File.ReadAllText(pluginConfigFilePath, Encoding.UTF8);
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.PropertyNameCaseInsensitive = true;
            pluginConfigModel = JsonSerializer.Deserialize<PluginConfigModel>(pluginConfigJsonStr, jsonSerializerOptions);

            return pluginConfigModel;
        }

        public static void Save(PluginConfigModel pluginConfigModel)
        {
            if (pluginConfigModel == null)
            {
                throw new ArgumentNullException(nameof(pluginConfigModel));
            }
            try
            {
                string pluginConfigJsonStr = JsonSerializer.Serialize(pluginConfigModel);
                string pluginConfigFilePath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "plugin.config.json");
                File.WriteAllText(pluginConfigFilePath, pluginConfigJsonStr, Encoding.UTF8);
            }
            catch (Exception ex)
            { }

        }
    }
}
