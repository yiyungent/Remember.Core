using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Framework.Plugins;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Common;

namespace WebApi.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class PluginsController : ControllerBase
    {


        #region 插件列表
        /// <summary>
        /// 加载插件列表
        /// </summary>
        /// <param name="status">插件状态</param>
        /// <returns></returns>
        public async Task<ActionResult<ResponseData>> List(string status = "all")
        {
            ResponseData responseData = new ResponseData();
            var pluginConfigModel = PluginConfigModelFactory.Create();

            // 获取所有插件信息
            IList<PluginInfoModelWrapper> pluginInfoModels = PluginInfoModelFactory.CreateAll();
            switch (status.ToLower())
            {
                case "all":
                    break;
                case "installed":
                    pluginInfoModels = pluginInfoModels.Where(m => pluginConfigModel.EnabledPlugins.Contains(m.PluginID) || pluginConfigModel.DisabledPlugins.Contains(m.PluginID)).ToList();
                    break;
                case "enabled":
                    pluginInfoModels = pluginInfoModels.Where(m => pluginConfigModel.EnabledPlugins.Contains(m.PluginID)).ToList();
                    break;
                case "disabled":
                    pluginInfoModels = pluginInfoModels.Where(m => pluginConfigModel.DisabledPlugins.Contains(m.PluginID)).ToList();
                    break;
                case "uninstalled":
                    pluginInfoModels = pluginInfoModels.Where(m => pluginConfigModel.UninstalledPlugins.Contains(m.PluginID)).ToList();
                    break;
                default:
                    break;
            }

            responseData.code = 1;
            responseData.message = "加载插件列表成功";
            responseData.data = pluginInfoModels;

            return await Task.FromResult(responseData);
        }
        #endregion

        #region 安装插件
        public async Task<ActionResult<ResponseData>> Install(string pluginId)
        {
            ResponseData responseData = new ResponseData();
            PluginConfigModel pluginConfigModel = PluginConfigModelFactory.Create();
            // TODO: 效验
            #region 效验

            #endregion

            // 1. 从 pluginConfigModel.UninstalledPlugins 移除, 添加到 pluginConfigModel.DisabledPlugins
            pluginConfigModel.UninstalledPlugins.Remove(pluginId);
            pluginConfigModel.DisabledPlugins.Add(pluginId);
            // 2.保存到 plugin.config.json
            PluginConfigModelFactory.Save(pluginConfigModel);
            // 3. 创建插件程序集加载上下文, 添加到 PluginsLoadContexts
            PluginsLoadContextsManager.LoadPlugin(pluginId);


            return await Task.FromResult(responseData);
        }
        #endregion

        #region 删除插件
        public async Task<ActionResult<ResponseData>> Delete(string pluginId)
        {
            ResponseData responseData = new ResponseData();
            var pluginConfigModel = PluginConfigModelFactory.Create();
            // 效验是否存在于 已卸载插件列表
            if (!pluginConfigModel.UninstalledPlugins.Contains(pluginId))
            {
                responseData.code = -1;
                responseData.message = "删除失败: 此插件不存在, 或未卸载";
                return await Task.FromResult(responseData);
            }
            // 1.从 pluginConfigModel.UninstalledPlugins 移除
            pluginConfigModel.UninstalledPlugins.Remove(pluginId);
            // 2.保存到 plugin.config.json
            PluginConfigModelFactory.Save(pluginConfigModel);
            // 3.删除物理文件
            try
            {
                string pluginPath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins", pluginId);
                var directory = new DirectoryInfo(pluginPath);
                directory.Delete(true);
                responseData.code = 1;
                responseData.message = "删除成功";
            }
            catch (Exception ex)
            {
                responseData.code = -2;
                responseData.message = "删除失败: " + ex.Message;
            }

            return await Task.FromResult(responseData);
        }
        #endregion

        #region 卸载插件
        public async Task<ActionResult<ResponseData>> Uninstall(string pluginId)
        {
            ResponseData responseData = new ResponseData();
            var pluginConfigModel = PluginConfigModelFactory.Create();
            // 卸载插件 必须 先禁用插件
            #region 效验
            if (pluginConfigModel.UninstalledPlugins.Contains(pluginId))
            {
                responseData.code = -3;
                responseData.message = "卸载失败: 此插件已卸载";
                return await Task.FromResult(responseData);
            }
            if (pluginConfigModel.EnabledPlugins.Contains(pluginId))
            {
                responseData.code = -1;
                responseData.message = "卸载失败: 请先禁用此插件";
                return await Task.FromResult(responseData);
            }
            if (!pluginConfigModel.DisabledPlugins.Contains(pluginId))
            {
                responseData.code = -2;
                responseData.message = "卸载失败: 此插件不存在";
                return await Task.FromResult(responseData);
            }
            #endregion
            // 1.从 pluginConfigModel.DisabledPlugins 移除, 添加到 pluginConfigModel.UninstalledPlugins
            pluginConfigModel.DisabledPlugins.Remove(pluginId);
            pluginConfigModel.UninstalledPlugins.Add(pluginId);
            // 2.保存到 plugin.config.json
            PluginConfigModelFactory.Save(pluginConfigModel);
            // 3.移除插件对应的程序集加载上下文
            PluginsLoadContextsManager.UnloadPlugin(pluginId);

            return await Task.FromResult(responseData);
        }
        #endregion

    }
}
