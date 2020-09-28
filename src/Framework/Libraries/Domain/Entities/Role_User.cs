using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public partial class Role_User : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// ÊÚÈ¨Ê±¼ä
        /// </summary>
        public DateTime? CreateTime { get; set; }

        #region Relationships

        public int UserInfoId { get; set; }
        [ForeignKey("UserInfoId")]
        public virtual UserInfo UserInfo { get; set; }

        public int RoleInfoId { get; set; }
        [ForeignKey("RoleInfoId")]
        public virtual RoleInfo RoleInfo { get; set; }

        #endregion

    }
}
