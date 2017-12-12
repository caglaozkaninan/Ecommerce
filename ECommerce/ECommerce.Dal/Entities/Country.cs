using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Dal.Entities
{
    [Table("Country")]
     public class Country
    {
       [Column("id")]
       [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string value { get; set; }

        [ForeignKey("addressid")]
        public virtual ICollection<Address> Addresses { get; set; }

    }
}
