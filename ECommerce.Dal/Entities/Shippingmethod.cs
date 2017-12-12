using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Dal.Entities
{
    [Table("Shippingmethod")]
    public class Shippingmethod
    {
        [Column("id")]
        [Key]
        public int id { get; set; }
        public string shippingmethodname { get; set; }
        public decimal price { get; set; }
    }
}
