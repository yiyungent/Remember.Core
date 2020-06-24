namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    [Serializable]
    public partial class UserInfo : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 用户名(唯一，可改，可作为登录使用)
        /// </summary>
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(60)]
        public string Password { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 最后登录Ip
        /// </summary>
        [StringLength(60)]
        public string LastLoginIp { get; set; }

        /// <summary>
        /// 最后登录地址
        /// </summary>
        [StringLength(60)]
        public string LastLoginAddress { get; set; }

        /// <summary>
        /// 选择的主体模板
        /// </summary>
        [StringLength(100)]
        [Column(TypeName = "text")]
        public string TemplateName { get; set; }

        /// <summary>
        /// 用户头像Url地址
        /// </summary>
        [StringLength(100)]
        [Column(TypeName = "text")]
        public string Avatar { get; set; }

        /// <summary>
        /// 邮箱(唯一，可改，可作为登录使用)
        /// </summary>
        [StringLength(30)]
        public string Email { get; set; }

        /// <summary>
        /// 手机号(唯一，可改，可作为登录使用)
        /// </summary>
        [StringLength(30)]
        public string Phone { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public int Credit { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(30)]
        public string Remark { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public UStatus UserStatus { get; set; }

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
        public virtual ICollection<Role_User> Role_Users { get; set; }

        #endregion

        #region Helpers

        [NotMapped]
        public IList<RoleInfo> RoleInfos
        {
            get
            {
                IList<RoleInfo> roleInfos = new List<RoleInfo>();
                if (this.Role_Users != null && this.Role_Users.Count >= 1)
                {
                    roleInfos = this.Role_Users.Select(m => m.RoleInfo).ToList();
                }

                return roleInfos;
            }
        }

        public enum UStatus
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal = 0,

            /// <summary>
            /// 冻结
            /// </summary>
            Frozen = 1,

            /// <summary>
            /// 限制访问
            /// </summary>
            Limited = 2
        }

        #endregion

    }
}
