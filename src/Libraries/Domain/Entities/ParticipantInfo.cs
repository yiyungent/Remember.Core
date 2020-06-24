using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// 实体类：参与者信息
    /// </summary>
    public partial class ParticipantInfo : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 担任角色数组
        /// eg: ["作词", "作曲", "后期"]
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(100)]
        public string RoleNames { get; set; }

        /// <summary>
        /// 参与描述
        /// <para>在此创作中做了什么</para>
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(1000)]
        public string Description { get; set; }

        /// <summary>
        /// 删除时间：为null，则未删除
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// 是否被删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
