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
            // 添加插件状态
            #region 添加插件状态信息
            foreach (var model in pluginInfoModels)
            {
                if (pluginConfigModel.EnabledPlugins.Contains(model.PluginId))
                {
                    model.Status = PluginStatus.Enabled;
                }
                else if (pluginConfigModel.DisabledPlugins.Contains(model.PluginId))
                {
                    model.Status = PluginStatus.Disabled;
                }
                else if (pluginConfigModel.UninstalledPlugins.Contains(model.PluginId))
                {
                    model.Status = PluginStatus.Uninstalled;
                }
            }
            #endregion
            #region 筛选插件状态
            switch (status.ToLower())
            {
                case "all":
                    break;
                case "installed":
                    pluginInfoModels = pluginInfoModels.Where(m => m.Status == PluginStatus.Enabled || m.Status == PluginStatus.Disabled).ToList();
                    break;
                case "enabled":
                    pluginInfoModels = pluginInfoModels.Where(m => m.Status == PluginStatus.Enabled).ToList();
                    break;
                case "disabled":
                    pluginInfoModels = pluginInfoModels.Where(m => m.Status == PluginStatus.Disabled).ToList();
                    break;
                case "uninstalled":
                    pluginInfoModels = pluginInfoModels.Where(m => m.Status == PluginStatus.Uninstalled).ToList();
                    break;
                default:
                    break;
            }
            #endregion

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
            if (string.IsNullOrEmpty(pluginId))
            {
                responseData.code = -1;
                responseData.message = "安装失败, pluginId不能为空";
                return await Task.FromResult(responseData);
            }
            #endregion

            try
            {
                // 1. 创建插件程序集加载上下文, 添加到 PluginsLoadContexts
                PluginsLoadContextsManager.LoadPlugin(pluginId);
                // 2. 从 pluginConfigModel.UninstalledPlugins 移除, 添加到 pluginConfigModel.DisabledPlugins
                pluginConfigModel.UninstalledPlugins.Remove(pluginId);
                pluginConfigModel.DisabledPlugins.Add(pluginId);
                // 3.保存到 plugin.config.json
                PluginConfigModelFactory.Save(pluginConfigModel);

                responseData.code = 1;
                responseData.message = "安装成功";
            }
            catch (Exception ex)
            {
                responseData.code = -1;
                responseData.message = "安装失败: " + ex.Message;
                return await Task.FromResult(responseData);
            }

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

            try
            {
                // 1.删除物理文件
                string pluginPath = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId);
                var directory = new DirectoryInfo(pluginPath);
                directory.Delete(true);
                // 2.从 pluginConfigModel.UninstalledPlugins 移除
                pluginConfigModel.UninstalledPlugins.Remove(pluginId);
                // 3.保存到 plugin.config.json
                PluginConfigModelFactory.Save(pluginConfigModel);

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

            try
            {
                // 1.移除插件对应的程序集加载上下文
                PluginsLoadContextsManager.UnloadPlugin(pluginId);
                // 2.从 pluginConfigModel.DisabledPlugins 移除, 添加到 pluginConfigModel.UninstalledPlugins
                pluginConfigModel.DisabledPlugins.Remove(pluginId);
                pluginConfigModel.UninstalledPlugins.Add(pluginId);
                // 3.保存到 plugin.config.json
                PluginConfigModelFactory.Save(pluginConfigModel);

                responseData.code = 1;
                responseData.message = "卸载成功";
            }
            catch (Exception ex)
            {
                responseData.code = -1;
                responseData.message = "卸载失败: " + ex.Message;
            }

            return await Task.FromResult(responseData);
        }
        #endregion

        #region 启用插件
        public async Task<ActionResult<ResponseData>> Enable(string pluginId)
        {
            ResponseData responseData = new ResponseData();
            var pluginConfigModel = PluginConfigModelFactory.Create();
            // 效验是否存在于 已禁用插件列表
            #region 效验
            if (!pluginConfigModel.DisabledPlugins.Contains(pluginId))
            {
                responseData.code = -1;
                responseData.message = "启用失败: 此插件不存在, 或未安装";
                return await Task.FromResult(responseData);
            }
            #endregion

            try
            {
                // 1.从 pluginConfigModel.DisabledPlugins 移除
                pluginConfigModel.DisabledPlugins.Remove(pluginId);
                // 2. 添加到 pluginConfigModel.EnabledPlugins
                pluginConfigModel.EnabledPlugins.Add(pluginId);
                // 3.保存到 plugin.config.json
                PluginConfigModelFactory.Save(pluginConfigModel);

                responseData.code = 1;
                responseData.message = "启用成功";
            }
            catch (Exception ex)
            {
                responseData.code = -2;
                responseData.message = "启用失败: " + ex.Message;
            }

            return await Task.FromResult(responseData);
        }
        #endregion

        #region 禁用插件
        public async Task<ActionResult<ResponseData>> Disable(string pluginId)
        {
            ResponseData responseData = new ResponseData();
            var pluginConfigModel = PluginConfigModelFactory.Create();
            // 效验是否存在于 已启用插件列表
            #region 效验
            if (!pluginConfigModel.EnabledPlugins.Contains(pluginId))
            {
                responseData.code = -1;
                responseData.message = "禁用失败: 此插件不存在, 或未安装";
                return await Task.FromResult(responseData);
            }
            #endregion

            try
            {
                // 1.从 pluginConfigModel.EnabledPlugins 移除
                pluginConfigModel.EnabledPlugins.Remove(pluginId);
                // 2. 添加到 pluginConfigModel.DisabledPlugins
                pluginConfigModel.DisabledPlugins.Add(pluginId);
                // 3.保存到 plugin.config.json
                PluginConfigModelFactory.Save(pluginConfigModel);

                responseData.code = 1;
                responseData.message = "禁用成功";
            }
            catch (Exception ex)
            {
                responseData.code = -2;
                responseData.message = "禁用失败: " + ex.Message;
            }

            return await Task.FromResult(responseData);
        }
        #endregion

        #region 上传插件
        /// <summary>
        /// 上传插件
        /// </summary>
        /// <param name="file">注意: 参数名一定为 file， 对应前端传过来时以 file 为名</param>
        /// <returns></returns>
        public async Task<ActionResult<ResponseData>> Upload([FromForm] IFormFile file)
        {
            ResponseData responseData = new ResponseData();

            #region 效验
            if (file == null)
            {
                responseData.code = -1;
                responseData.message = "上传的文件不能为空";
                return responseData;
            }
            //文件后缀
            string fileExtension = Path.GetExtension(file.FileName);//获取文件格式，拓展名
            if (fileExtension != ".zip")
            {
                responseData.code = -1;
                responseData.message = "只能上传zip格式文件";
                return responseData;
            }
            //判断文件大小
            var fileSize = file.Length;
            if (fileSize > 1024 * 1024 * 5) // 5M
            {
                responseData.code = -1;
                responseData.message = "上传的文件不能大于5MB";
                return responseData;
            }
            #endregion

            try
            {
                // 1.上传到 Plugins 目录
                //文件保存
                string pluginsRootPath = PluginPathProvider.PluginsRootPath();
                string pluginZipPath = Path.Combine(pluginsRootPath, file.FileName);
                using (var fs = System.IO.File.Create(pluginZipPath))
                {
                    file.CopyTo(fs); //将上传的文件文件流，复制到fs中
                    fs.Flush();//清空文件流
                }
                // 3.解压
                bool isDecomparessSuccess = Core.Common.ZipHelper.DecomparessFile(pluginZipPath, pluginZipPath.Replace(".zip", ""));
                // 4.删除原压缩包
                System.IO.File.Delete(pluginZipPath);
                // 5. 加入 PluginConfigModel.UninstalledPlugins
                string pluginId = file.FileName.Replace(".zip", "");
                PluginConfigModel pluginConfigModel = PluginConfigModelFactory.Create();
                pluginConfigModel.UninstalledPlugins.Add(pluginId);
                PluginConfigModelFactory.Save(pluginConfigModel);

                responseData.code = 1;
                responseData.message = "上传插件成功";
            }
            catch (Exception ex)
            {
                responseData.code = -1;
                responseData.message = "上传插件失败: " + ex.Message;
            }

            return await Task.FromResult(responseData);
        }
        #endregion
    }
}
