using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public partial class Role_Permission : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// ÊÚÈ¨Ê±¼ä
        /// </summary>
        public DateTime? CreateTime { get; set; }

        #region Relationships

        public int RoleInfoId { get; set; }
        [ForeignKey("RoleInfoId")]
        public virtual RoleInfo RoleInfo { get; set; }

        public int PermissionInfoId { get; set; }
        [ForeignKey("PermissionInfoId")]
        public virtual PermissionInfo PermissionInfo { get; set; }

        #endregion
    }
}
