using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Dal.Entities
{
    [Table("Basket")]
    public class Basket
    {

        [Column("id")]
        [Key]
        public int id { get; set; }
        public DateTime createdtime { get; set; }
        public decimal totalprice { get; set; }
        public int? userid { get; set; }
        public int? shippingaddressid { get; set; }
        public int? billingaddressid { get; set; }
        public int? paymentmethodid { get; set; }
        public int? shippingmethodid { get; set; }

        [ForeignKey("shippingaddressid ")]
        public virtual Address shippingAddress { get; set; }

        [ForeignKey("billingaddressid ")]
        public virtual Address billingAddress { get; set; }

        [ForeignKey("shippingmethodid")]
        public virtual Shippingmethod shippingmethod { get; set; }

        [ForeignKey("paymentmethodid")]
        public virtual Paymentmethod paymentmethod { get; set; }

        [ForeignKey("userid")]
        public virtual User User { get; set; }
        
        public virtual ICollection<Basket_item> Basket_items { get; set; }

    }
}
