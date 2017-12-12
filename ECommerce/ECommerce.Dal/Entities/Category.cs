using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Dal.Entities
{
    [Table("Category")]
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool isdeleted { get; set; }
        public string picture { get; set; }

        [ForeignKey("categoryid")]
        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}
