using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Project: remember
// https://github.com/yiyungent/remember
// Author: yiyun <yiyungent@gmail.com>
namespace Domain.Entities
{
    /// <summary>
    /// 消息信息
    /// </summary>
    public partial class Message : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime { get; set; }

        /// <summary>
        /// 收信人 读取时间，为0则未读
        /// </summary>
        public long ReadTime { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(8000)]
        public string Content { get; set; }

        /// <summary>
        /// 发信人
        /// </summary>
        [ForeignKey("Sender")]
        public int SenderId { get; set; }
        [ForeignKey("SenderId")]
        public virtual UserInfo Sender { get; set; }

        /// <summary>
        /// 收信人
        /// </summary>
        [ForeignKey("Receiver")]
        public int ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public virtual UserInfo Receiver { get; set; }
    }
}
