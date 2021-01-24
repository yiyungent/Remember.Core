using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


// Project: remember
// https://github.com/yiyungent/remember
// Author: yiyun <yiyungent@gmail.com>
namespace Domain.Entities
{
    /// <summary>
    /// 分类
    /// </summary>
    public partial class CatInfo : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 分类名
        /// </summary>
        [StringLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [StringLength(500)]
        [Column(TypeName = "text")]
        public string Icon { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public int SortCode { get; set; }

        /// <summary>
        /// 上级分类
        ///     多对一关系
        /// </summary>
        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual CatInfo Parent { get; set; }

        /// <summary>
        /// 子分类列表
        ///     一对多关系
        /// </summary>
        [InverseProperty("Parent")]
        public virtual ICollection<CatInfo> Children { get; set; }

    }
}
