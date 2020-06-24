namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ÊÕ²Ø¼Ð-ÎÄÕÂ
    /// </summary>
    public partial class Favorite_Article : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        public DateTime CreateTime { get; set; }

        #region Relationships

        [ForeignKey("Favorite")]
        public int FavoriteId { get; set; }
        [ForeignKey("FavoriteId")]
        public virtual Favorite Favorite { get; set; }

        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }

        #endregion
    }
}
