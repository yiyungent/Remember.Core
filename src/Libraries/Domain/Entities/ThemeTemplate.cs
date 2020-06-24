namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ThemeTemplate : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 模板名(标识，唯一)
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column(TypeName = "text")]
        public string TemplateName { get; set; }

        /// <summary>
        /// 模板标题（展示名）
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column(TypeName = "text")]
        public string Title { get; set; }

        /// <summary>
        /// 状态
        ///     0: 禁用
        ///     1: 开启
        /// </summary>
        public int IsOpen { get; set; }
    }
}
