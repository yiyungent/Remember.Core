namespace WebApi.Models.Article
{
    public class ArticleResponseModel
    {
        public ArticleItem Article { get; set; }

        public int RankingNum { get; set; }

        public sealed class ArticleItem
        {
            /// <summary>
            /// ID
            /// </summary>
            public int ID { get; set; }

            /// <summary>
            /// 文章标题
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 描述
            /// </summary>
            public string Desc { get; set; }

            /// <summary>
            /// 创建者
            /// </summary>
            public Author Author { get; set; }

            /// <summary>
            /// 封面图
            /// </summary>
            public string PicUrl { get; set; }

            /// <summary>
            /// js时间戳
            /// </summary>
            public long CreateTime { get; set; }
        }

        public sealed class Author
        {
            public int ID { get; set; }

            public string UserName { get; set; }

            public string Desc { get; set; }

            public string Avatar { get; set; }
        }
    }
}
