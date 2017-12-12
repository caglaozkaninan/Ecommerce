using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Dal.Entities
{
    [Table("sub_category")]
    public class Subcategory
    {
        [Column("id")]
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public bool isdeleted { get; set; }
        public int categoryid { get; set; }

        [ForeignKey("categoryid")]
        public virtual Category Category { get; set; }
    }
}
