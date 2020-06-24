namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    /// <summary>
    /// 收藏夹
    /// </summary>
    public partial class Favorite : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 收藏夹名
        /// </summary>
        [StringLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// 收藏夹描述
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 删除时间：为null，则未删除
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// 是否被删除
        /// </summary>
        public bool IsDeleted { get; set; }

        #region Relationships

        /// <summary>
        /// 收藏夹的创建者
        /// </summary>
        [ForeignKey("Creator")]
        public int CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public virtual UserInfo Creator { get; set; }

        /// <summary>
        /// 收藏的文章列表
        /// </summary>
        public virtual ICollection<Favorite_Article> Favorite_Articles { get; set; }

        #endregion

        #region Helpers

        [NotMapped]
        public IList<Article> Articles
        {
            get
            {
                IList<Article> bookInfos = new List<Article>();
                if (this.Favorite_Articles != null && this.Favorite_Articles.Count >= 1)
                {
                    bookInfos = this.Favorite_Articles.Select(m => m.Article)?.ToList();
                }

                return bookInfos;
            }
        }

        #endregion
    }
}
