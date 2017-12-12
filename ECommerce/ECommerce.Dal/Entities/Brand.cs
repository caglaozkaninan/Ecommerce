using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Dal.Entities
{
    [Table("Brand")]
    public class Brand
    {
        public int id { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public bool isdeleted { get; set; }
    }
}
