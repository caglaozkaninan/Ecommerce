using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Dal.Entities
{
    [Table("basket_item")]
    public class Basket_item
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        [Key]
        public int id { get; set; }

        public int productid { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public DateTime addeddate { get; set; }

        public int basketid { get; set; }

        [ForeignKey("basketid")]
        public virtual Basket Basket { get; set; }

        [ForeignKey("productid")]
        public virtual Product Product { get; set; }
    }
}
