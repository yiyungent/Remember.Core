using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Admin.Article
{
    public class ArticleListResponseModel
    {
        public IList<ArticleItemModel> List { get; set; }

        /// <summary>
        /// 当前第几页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页多少条
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总条数目
        /// </summary>
        public int TotalCount { get; set; }

        public ArticleListResponseModel()
        {
            this.List = new List<ArticleItemModel>();
        }

        public class ArticleItemModel
        {
            public int Id { get; set; }

            public AuthorModel Author { get; set; }

            public string Title { get; set; }

            public long CreateTime { get; set; }

            public long LastUpdateTime { get; set; }

            public string CustomUrl { get; set; }

            /// <summary>
            /// 文章分类
            /// </summary>
            public CatModel Cat { get; set; }
        }

        public class AuthorModel
        {
            public int Id { get; set; }

            public string UserName { get; set; }
        }

        public class CatModel
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}
