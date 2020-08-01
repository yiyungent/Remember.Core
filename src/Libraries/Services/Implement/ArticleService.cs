using Domain.Entities;
using Repositories.Interface;
using Services.Core;
using Services.Interface;
using System.Collections.Generic;

namespace Services.Implement
{
    public partial class ArticleService : BaseService<Article>, IArticleService
    {
        /// <summary>
        /// 查询首页文章列表
        /// </summary>
        /// <param name="limit">要查询的记录数</param>
        /// <returns></returns>
        public IEnumerable<Article> FindHomePageArticles(int limit = 20)
        {
            return _repository.FindHomePageArticles(limit);
        }
    }
}
