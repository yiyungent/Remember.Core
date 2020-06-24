namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    /// <summary>
    /// 实体类: 角色
    /// </summary>
    [Serializable]
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
        public virtual ICollection<Role_Function> Role_Functions { get; set; }

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
        public ICollection<FunctionInfo> FunctionInfos
        {
            get
            {
                ICollection<FunctionInfo> functionInfos = new List<FunctionInfo>();
                if (this.Role_Functions != null && this.Role_Functions.Count >= 1)
                {
                    functionInfos = this.Role_Functions.Select(m => m.FunctionInfo).ToList();
                }

                return functionInfos;
            }
        }

        #endregion
    }
}
