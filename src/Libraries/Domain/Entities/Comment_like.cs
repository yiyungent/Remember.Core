using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// 评论-赞的人
    /// </summary>
    public partial class Comment_Like : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        #region Relationships

        /// <summary>
        /// 评论
        /// </summary>
        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }

        /// <summary>
        /// 赞此评论的人
        /// </summary>
        [ForeignKey("UserInfo")]
        public int UserInfoId { get; set; }
        [ForeignKey("UserInfoId")]
        public virtual UserInfo UserInfo { get; set; }

        #endregion
    }
}
