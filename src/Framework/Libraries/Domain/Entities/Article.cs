using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public partial class Article : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(30)]
        public string Title { get; set; }

        /// <summary>
        /// 描述/摘要
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(1000)]
        public string Description { get; set; }

        /// <summary>
        /// 封面图
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(1000)]
        public string PicUrl { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(8000)]
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最近更新
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 自定义Url
        /// </summary>
        [StringLength(1000)]
        [Column(TypeName = "text")]
        public string CustomUrl { get; set; }

        /// <summary>
        /// 赞数
        /// </summary>
        public int LikeNum { get; set; }

        /// <summary>
        /// 踩数
        /// </summary>
        public int DislikeNum { get; set; }

        /// <summary>
        /// 分享数
        /// </summary>
        public int ShareNum { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int CommentNum { get; set; }

        /// <summary>
        /// 收藏数目
        /// </summary>
        public int FavNum { get; set; }

        /// <summary>
        /// 文章状态
        /// </summary>
        public AStatus ArticleStatus { get; set; }

        /// <summary>
        /// 评论状态
        /// </summary>
        public CStatus CommentStatus { get; set; }

        /// <summary>
        /// 可见程度
        /// </summary>
        public OStatus OpenStatus { get; set; }

        #region Relationships

        /// <summary>
        /// 作者
        /// </summary>
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual UserInfo Author { get; set; }

        #endregion

        public enum AStatus
        {
            /// <summary>
            /// 被发布
            /// </summary>
            Publish = 0,

            /// <summary>
            /// 编辑中（草稿状态）
            /// </summary>
            Draft = 1,
        }

        public enum CStatus
        {
            /// <summary>
            /// 允许评论
            /// </summary>
            Open,

            /// <summary>
            /// 关闭评论
            /// </summary>
            Closed,
        }

        public enum OStatus
        {
            /// <summary>
            /// 所有人可见
            /// </summary>
            All,

            /// <summary>
            /// 仅自己可见
            /// </summary>
            Self,
            /// <summary>
            /// 自己和粉丝可见
            /// </summary>
            Fans
        }
    }
}
