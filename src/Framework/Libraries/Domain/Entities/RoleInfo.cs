using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.Entities
{
    /// <summary>
    /// 实体类: 角色
    /// </summary>
    public partial class RoleInfo : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        [StringLength(500)]
        public string Remark { get; set; }

        #region Relationships

        /// <summary>
        /// 角色-用户
        /// </summary>
        [InverseProperty("RoleInfo")]
        public virtual ICollection<Role_User> Role_Users { get; set; }

        /// <summary>
        /// 角色-菜单
        /// </summary>
        [InverseProperty("RoleInfo")]
        public virtual ICollection<Role_Menu> Role_Menus { get; set; }

        /// <summary>
        /// 角色-权限
        /// </summary>
        [InverseProperty("RoleInfo")]
        public virtual ICollection<Role_Permission> Role_Permissions { get; set; }

        #endregion

        #region Helpers

        [NotMapped]
        public ICollection<UserInfo> UserInfos
        {
            get
            {
                ICollection<UserInfo> userInfos = new List<UserInfo>();
                if (this.Role_Users != null && this.Role_Users.Count >= 1)
                {
                    userInfos = this.Role_Users.Select(m => m.UserInfo).ToList();
                }

                return userInfos;
            }
        }

        [NotMapped]
        public ICollection<Sys_Menu> Sys_Menus
        {
            get
            {
                ICollection<Sys_Menu> sys_Menus = new List<Sys_Menu>();
                if (this.Role_Menus != null && this.Role_Menus.Count >= 1)
                {
                    sys_Menus = this.Role_Menus.Select(m => m.Sys_Menu).ToList();
                }

                return sys_Menus;
            }
        }

        [NotMapped]
        public ICollection<PermissionInfo> PermissionInfos
        {
            get
            {
                ICollection<PermissionInfo> permissionInfos = new List<PermissionInfo>();
                if (this.Role_Permissions != null && this.Role_Permissions.Count >= 1)
                {
                    permissionInfos = this.Role_Permissions.Select(m => m.PermissionInfo).ToList();
                }

                return permissionInfos;
            }
        }

        #endregion
    }
}
