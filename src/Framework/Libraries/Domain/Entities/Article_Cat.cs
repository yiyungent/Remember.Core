using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Project: remember
// https://github.com/yiyungent/remember
// Author: yiyun <yiyungent@gmail.com>
namespace Domain.Entities
{
    /// <summary>
    /// 文章的分类
    /// </summary>
    public partial class Article_Cat : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        #region Relationships

        public int CatInfoId { get; set; }
        [ForeignKey("CatInfoId")]
        public virtual CatInfo CatInfo { get; set; }

        public int ArticleId { get; set; }
        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }

        #endregion
    }
}
