using Domain.Entities;
using Services.Core;
using System.Collections.Generic;

namespace Services.Interface
{
    public partial interface IArticleService : IService<Article>
    {
        /// <summary>
        /// 查询首页文章列表
        /// </summary>
        /// <param name="limit">要查询的记录数</param>
        /// <returns></returns>
        IEnumerable<Article> FindHomePageArticles(int limit = 20);
    }
}
