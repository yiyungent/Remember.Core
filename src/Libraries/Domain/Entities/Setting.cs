namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Setting : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column(TypeName = "text")]
        public string SetKey { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(500)]
        public string SetValue { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        [StringLength(100)]
        [Column(TypeName = "text")]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(1000)]
        public string Remark { get; set; }

    }
}
