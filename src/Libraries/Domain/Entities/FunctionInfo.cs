using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public partial class FunctionInfo : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 权限键（唯一标识）
        /// </summary>
        [Required]
        [StringLength(50)]
        public string AuthKey { get; set; }

        /// <summary>
        /// 操作名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(50)]
        public string Remark { get; set; }

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
        /// 系统菜单
        ///     多对一关系
        /// </summary>
        [ForeignKey("Sys_Menu")]
        public int? Sys_MenuId { get; set; }
        [ForeignKey("Sys_MenuId")]
        public virtual Sys_Menu Sys_Menu { get; set; }

        /// <summary>
        /// 角色-权限
        /// </summary>
        public virtual ICollection<Role_Function> Role_Functions { get; set; }

        #endregion
    }
}
