using Domain.Entities;
using Repositories.Core;
using System.Linq;

namespace Repositories.Interface
{
    public partial interface IArticleRepository : IRepository<Article>
    {
        /// <summary>
        /// 查询首页文章列表
        /// </summary>
        /// <param name="limit">要查询的记录数</param>
        /// <returns></returns>
        IQueryable<Article> FindHomePageArticles(int limit = 20);
    }
}
