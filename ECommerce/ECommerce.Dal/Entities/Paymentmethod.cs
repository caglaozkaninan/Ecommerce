using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Dal.Entities
{
    [Table("Paymentmethod")]
    public  class Paymentmethod
    {
        [Column("id")]
        [Key]
        public int id { get; set; }
        public string paymentmethodname { get; set; }
    }
}
