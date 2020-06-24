namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 实体类：日志信息
    /// </summary>
    public partial class LogInfo : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 访问者的用户ID
        /// 如果未登录，则为 0
        /// </summary>
        public int AccessUserId { get; set; }

        /// <summary>
        /// 访客识别码：浏览器指纹
        /// </summary>
        [StringLength(100)]
        [Column(TypeName = "text")]
        public string IdCode { get; set; }

        /// <summary>
        /// 访问者的IP
        /// </summary>
        [StringLength(30)]
        public string AccessIp { get; set; }

        /// <summary>
        /// 访问者所在城市
        /// </summary>
        [StringLength(500)]
        [Column(TypeName = "text")]
        public string AccessCity { get; set; }

        /// <summary>
        /// 解析 UserAgent json字符串
        /// {
        ///      ua: "",
        ///      browser: {
        ///          name: "",
        ///          version: ""
        ///      },
        ///      engine: {
        ///          name: "",
        ///          version: ""
        ///      },
        ///      os: {
        ///          name: "",
        ///          version: ""
        ///      },
        ///      device: {
        ///          model: "",
        ///          type: "",
        ///          vendor: ""
        ///      },
        ///      cpu: {
        ///          architecture: ""
        ///      }
        /// }
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(500)]
        public string UserAgent { get; set; }

        [StringLength(30)]
        public string Browser { get; set; }

        [StringLength(30)]
        public string BrowserEngine { get; set; }

        [StringLength(30)]
        public string OS { get; set; }

        [StringLength(30)]
        public string Device { get; set; }

        [StringLength(30)]
        public string Cpu { get; set; }

        /// <summary>
        /// 访客信息
        /// {
        ///      screen: {
        ///          width: 1280,
        ///          height: 720
        ///      },
        ///      
        /// }
        /// </summary>
        [StringLength(500)]
        [Column(TypeName = "text")]
        public string VisitorInfo { get; set; }

        /// <summary>
        /// 页面点击次数
        /// </summary>
        public int ClickCount { get; set; }

        /// <summary>
        /// 访问时间：进入网页，加载完的时间
        /// </summary>
        public DateTime AccessTime { get; set; }

        /// <summary>
        /// 跳出网页时间
        /// </summary>
        public DateTime JumpTime { get; set; }

        /// <summary>
        /// 在页面的持续时间 = JumpTime - AccessTime
        /// 总秒数
        /// </summary>
        public long Duration { get; set; }

        /// <summary>
        /// 访问地址
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(1000)]
        public string AccessUrl { get; set; }

        /// <summary>
        /// 来源URL
        /// </summary>
        [Column(TypeName = "text")]
        [StringLength(1000)]
        public string RefererUrl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
