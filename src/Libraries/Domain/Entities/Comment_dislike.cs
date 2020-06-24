namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 评论-踩的人
    /// </summary>
    public partial class Comment_Dislike : BaseEntity
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
        /// 踩此评论的人
        /// </summary>
        [ForeignKey("UserInfo")]
        public int UserInfoId { get; set; }
        [ForeignKey("UserInfoId")]
        public virtual UserInfo UserInfo { get; set; }

        #endregion
    }
}
