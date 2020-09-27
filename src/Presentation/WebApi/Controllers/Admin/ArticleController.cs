using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using Framework.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PluginCore;
using PluginCore.Models;
using Services.Interface;
using WebApi.Models.Admin.Article;
using WebApi.Models.Common;

namespace WebApi.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [WebApiAuthorize]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        #region Fields

        private readonly IArticleService _articleService;

        private readonly IArticle_CatService _article_CatService;

        private readonly ICatInfoService _catInfoService;

        #endregion

        #region Ctor
        public ArticleController(IArticleService articleService, IArticle_CatService article_CatService, ICatInfoService catInfoService)
        {
            this._articleService = articleService;
            this._article_CatService = article_CatService;
            this._catInfoService = catInfoService;
        }
        #endregion

        #region Actions

        #region 列表
        /// <summary>
        /// 加载列表
        /// </summary>
        public async Task<ActionResult<ResponseModel>> List(int pageIndex = 1, int pageSize = 6)
        {
            ResponseModel responseData = new ResponseModel();
            ArticleListResponseModel responseModel = new ArticleListResponseModel();
            responseModel.PageIndex = pageIndex;
            responseModel.PageSize = pageSize;
            var articlePageModel = await this._articleService.FilterAsync(pageIndex, pageSize, m => !m.IsDeleted, o => o.ID, false);
            articlePageModel.Data = articlePageModel.Data.Include(m => m.Author);
            var allCat = (await this._article_CatService.AllAsync()).Include(m => m.Article).Include(m => m.CatInfo).Include(m => m.CatInfo.Parent).Include(m => m.CatInfo.Children).ToList();

            responseModel.TotalCount = articlePageModel.TotalCount;
            foreach (var item in articlePageModel.Data)
            {
                responseModel.List.Add(new ArticleListResponseModel.ArticleItemModel
                {
                    Id = item.ID,
                    CreateTime = item.CreateTime.ToTimeStamp10(),
                    LastUpdateTime = item.LastUpdateTime.ToTimeStamp10(),
                    Title = item.Title,
                    CustomUrl = item.CustomUrl,
                    Author = new ArticleListResponseModel.AuthorModel
                    {
                        Id = item.AuthorId,
                        UserName = item.Author.UserName
                    },
                    Cat = new ArticleListResponseModel.CatModel
                    {
                        Id = allCat.FirstOrDefault(m => m.ArticleId == item.ID)?.CatInfoId ?? 0,
                        Name = allCat.FirstOrDefault(m => m.ArticleId == item.ID)?.CatInfo?.Name ?? ""
                    },
                });
            }

            responseData.code = 1;
            responseData.message = "加载列表成功";
            responseData.data = responseModel;

            return await Task.FromResult(responseData);
        }
        #endregion

        #region 创建
        public async Task<ActionResult<ResponseModel>> Create()
        {
            
        }
        #endregion

        #endregion
    }
}
