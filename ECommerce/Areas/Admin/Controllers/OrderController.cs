using ECommerce.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Areas.Admin.Controllers
{
    [RouteArea("Admin", AreaPrefix = "admin")]
    [RoutePrefix("order")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        ECommerce.Dal.ECommerceDbContext context = new Dal.ECommerceDbContext();
        // GET: Admin/User
        [Route("index" ,Name ="order")]
        public ActionResult Index()
        {
            List<OrderViewModel> items = context.Baskets.Where(x=>x.billingaddressid != null).Select(x => new OrderViewModel()
            {
                BasketId = x.id,
                CreatedTime = x.createdtime,
                TotalPrice = x.totalprice,
                UserName = x.User.name + " " + x.User.surname,
                PaymentMethodName = x.paymentmethod.paymentmethodname,
                BillingAddressName = x.billingAddress.name,
                ShippingAddressName = x.shippingAddress.name,
                ShippingMethodName = x.shippingmethod.shippingmethodname,

            }).ToList();
            return View(items);
        }
    }
}