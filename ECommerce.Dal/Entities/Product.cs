using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Dal.Entities
{
    [Table("Product")]
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string specification { get; set; }
        public decimal price { get; set; }
        public int categoryid { get; set; }
        public int brandid { get; set; }
        public bool isdeleted { get; set; }

        public int stockquantity { get; set; }
        public string picturebig { get; set; }
        public string picturesmall { get; set; }

        [ForeignKey("categoryid")]
        public virtual Subcategory Category { get; set; }

        [ForeignKey("brandid")]
        public virtual Brand Brand { get; set; }

    }
}
