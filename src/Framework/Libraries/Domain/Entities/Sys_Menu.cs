using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public partial class Sys_Menu : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 菜单名
        /// </summary>
        [Required]
        [StringLength(500)]
        [Column(TypeName = "text")]
        public string Name { get; set; }

        /// <summary>
        /// 菜单描述
        /// </summary>
        [StringLength(500)]
        [Column(TypeName = "text")]
        public string Description { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [StringLength(500)]
        [Column(TypeName = "text")]
        public string Icon { get; set; }

        /// <summary>
        /// 控制器名
        /// </summary>
        [StringLength(500)]
        [Column(TypeName = "text")]
        public string ControllerName { get; set; }

        /// <summary>
        /// 动作名
        /// </summary>
        [StringLength(500)]
        [Column(TypeName = "text")]
        public string ActionName { get; set; }

        /// <summary>
        /// 区域名
        /// </summary>
        [StringLength(500)]
        [Column(TypeName = "text")]
        public string AreaName { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public int SortCode { get; set; }

        #region Relationships

        /// <summary>
        /// 上级菜单
        ///     多对一关系
        /// </summary>
        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Sys_Menu Parent { get; set; }

        /// <summary>
        /// 子菜单列表
        ///     一对多关系
        /// </summary>
        [InverseProperty("Parent")]
        public virtual ICollection<Sys_Menu> Children { get; set; }

        /// <summary>
        /// 角色-菜单
        /// </summary>
        [InverseProperty("Sys_Menu")]
        public virtual ICollection<Role_Menu> Role_Menus { get; set; }

        #endregion
    }
}
