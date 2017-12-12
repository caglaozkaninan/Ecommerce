using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Dal.Entities
{
    [Table("Address")]
    public class Address
    {
        [Column("id")]
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public int postcode { get; set; }
        public int countryid { get; set; }
        public bool isdeleted { get; set; }
        public int userid { get; set; }

        [ForeignKey("userid")]
        public virtual User User { get; set; }

        [ForeignKey("countryid")]
        public virtual Country country { get; set; }

    }
}
