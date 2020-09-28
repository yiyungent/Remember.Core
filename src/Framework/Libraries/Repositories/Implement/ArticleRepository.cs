using Domain.Entities;
using Repositories.Core;
using Repositories.Interface;
using System.Linq;

namespace Repositories.Implement
{
    public partial class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        public IQueryable<Article> FindHomePageArticles(int limit = 20)
        {
            return this._context.Article.OrderByDescending(m => m.CreateTime).Take(limit);
        }
    }
}
