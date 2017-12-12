using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Areas.Admin.Models
{
    public class SiparisViewModel
    {
        public int BasketId { get; set; }
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public string ShippingAddressName { get; set; }
        public string BillingAddressName { get; set; }
        public string ShippingMethodName { get; set; }
        public string PaymentMethodName { get; set; }
        public DateTime CreatedTime { get; set; }
        public int BasketItemCount { get; set; }


    }
}