using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Common;
using Domain;
using Domain.Entities;
using Services.Interface;
using WebApi.Models.Article;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        #region Fields
        private readonly IArticleService _articleService;
        #endregion

        #region Ctor

        public ArticleController(IArticleService articleService)
        {
            this._articleService = articleService;
        }
        #endregion

        #region Actions

        [HttpGet, Route(nameof(Last))]
        public async Task<ActionResult<ResponseModel>> Last(int number)
        {
            ResponseModel responseData = null;
            try
            {
                IList<ArticleResponseModel> viewModel = new List<ArticleResponseModel>();
                IList<Article> articles = new List<Article>();
                var e = await this._articleService.FilterAsync<DateTime>(1, number, m => true, m => m.CreateTime, false);
                articles = e.Data.ToList();


                for (int i = 0; i < articles.Count; i++)
                {
                    Article article = articles[i];

                    viewModel.Add(new ArticleResponseModel
                    {
                        Article = new ArticleResponseModel.ArticleItem
                        {
                            ID = article.ID,
                            CreateTime = article.CreateTime.ToTimeStamp13(),
                            Author = new ArticleResponseModel.Author
                            {
                                ID = article.Author.ID,
                                Avatar = article.Author.Avatar/*.ToHttpAbsoluteUrl()*/,
                                Desc = article.Author.Description,
                                UserName = article.Author.UserName
                            },
                            Desc = article.Description,
                            Title = article.Title,
                            PicUrl = article.PicUrl/*.ToHttpAbsoluteUrl()*/
                        },
                        RankingNum = i + 1
                    });
                }


                responseData = new ResponseModel
                {
                    code = 1,
                    message = "成功获取最新文章",
                    data = viewModel
                };
            }
            catch (Exception ex)
            {
                responseData = new ResponseModel
                {
                    code = -1,
                    message = "获取最新文章失败 " + ex.Message + " " + ex.InnerException?.Message
                };
            }

            return responseData;
        }

        [HttpGet, Route(nameof(Hot))]
        public async Task<ActionResult<ResponseModel>> Hot(int number)
        {
            ResponseModel responseData = null;
            try
            {
                IList<ArticleResponseModel> viewModel = new List<ArticleResponseModel>();
                IList<Article> articles = new List<Article>();

                var e = await this._articleService.FilterAsync<int>(1, number, m => true, m => m.LikeNum, false);
                articles = e.Data.ToList();


                for (int i = 0; i < articles.Count; i++)
                {
                    Article article = articles[i];

                    viewModel.Add(new ArticleResponseModel
                    {
                        Article = new ArticleResponseModel.ArticleItem
                        {
                            ID = article.ID,
                            CreateTime = article.CreateTime.ToTimeStamp13(),
                            Author = new ArticleResponseModel.Author
                            {
                                ID = article.Author.ID,
                                Avatar = article.Author.Avatar/*.ToHttpAbsoluteUrl()*/,
                                Desc = article.Author.Description,
                                UserName = article.Author.UserName
                            },
                            Desc = article.Description,
                            Title = article.Title,
                            PicUrl = article.PicUrl/*.ToHttpAbsoluteUrl()*/
                        },
                        RankingNum = i + 1
                    });
                }


                responseData = new ResponseModel
                {
                    code = 1,
                    message = "成功获取最热文章",
                    data = viewModel
                };
            }
            catch (Exception ex)
            {
                responseData = new ResponseModel
                {
                    code = -1,
                    message = "获取最热文章失败 " + ex.Message + " " + ex.InnerException?.Message
                };
            }

            return responseData;
        }

        #endregion
    }
}
