using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Dal.Entities
{
    [Table("UserRole")]
    public class UserRole
    {
        [Column("id")]
        [Key]
        public int id { get; set; }
        public string rolename { get; set; }
        public string r_delete { get; set; }
        public string r_update { get; set; }
        public string r_add { get; set; }

        [ForeignKey("roleid")]
        public virtual ICollection<User> Users { get; set; }
    }
}
