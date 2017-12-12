using ECommerce.Dal.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ECommerce.Models
{
    public class CheckOutViewModel
    {
        public int UserId { get; set; }
        public int BasketId { get; set; }
        public User UserDetail { get; set; }
        public IEnumerable<SelectListItem> Addresses { get; set; }
        public IEnumerable<Shippingmethod> ShippingMethods { get; set; }
        public IEnumerable<SelectListItem> PaymentMethods { get; set; }
        public IEnumerable<Basket_item> BasketItems { get; set; }
        public int SelectedBillingAddressId { get; set; }
        public int SelectedShippingAddressId { get; set; }
        public int SelectedPaymentMethodId { get; set; }
        public int SelectedShippingMethodId { get; set; }
    }
}