using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public partial class Role_Function : BaseEntity
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

        public int FunctionInfoId { get; set; }
        [ForeignKey("FunctionInfoId")]
        public virtual FunctionInfo FunctionInfo { get; set; }

        #endregion
    }
}
