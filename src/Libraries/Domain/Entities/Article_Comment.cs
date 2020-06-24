using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Domain.Entities
{
    public partial class Article_Comment : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        #region Relationships

        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }

        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }

        #endregion
    }
}
