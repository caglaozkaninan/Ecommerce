using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Dal.Entities
{
    [Table("User")]
    public class User
    {
        [Column("id")]
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string phone { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public int roleid { get; set; }

        [ForeignKey("roleid")]
        public virtual UserRole Role { get; set; }

        [ForeignKey("userid")]
        public virtual ICollection<Address> Addresses { get; set; }

        [ForeignKey("userid")]
        public virtual ICollection<Basket> Baskets { get; set; }
    }
}
