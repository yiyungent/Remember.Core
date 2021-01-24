using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public partial class Comment : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(5000)]
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最近更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 赞的人数
        /// </summary>
        public int LikeNum { get; set; }

        /// <summary>
        /// 踩的人数
        /// </summary>
        public int DislikeNum { get; set; }

        #region Relationships

        /// <summary>
        /// 作者
        /// </summary>
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual UserInfo Author { get; set; }

        /// <summary>
        /// 此条评论回复了谁
        /// </summary>
        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Comment Parent { get; set; }

        /// <summary>
        /// 有哪些评论回复了此条评论
        /// </summary>
        [InverseProperty("Parent")]
        public virtual ICollection<Comment> Children { get; set; }

        #endregion
    }
}
