using ECommerce.Dal;
using ECommerce.Dal.Entities;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class BasketController : Controller
    {
        private ECommerceDbContext context = new ECommerceDbContext();

        [Route("basket/checkout/{basketid}")]
        [Authorize(Roles = "User")]
        //bu sayade kullanıcı otomatik olarak login ekranına 
        //gönderilecek eğer kullanıcı zaten loginse gerek kalmayacak
        public ActionResult Checkout(int basketid)
        {
            CheckOutViewModel viewModel = new CheckOutViewModel();

            viewModel.BasketId = basketid;

            viewModel.PaymentMethods = context.Paymentmethods.Select(x => new SelectListItem
            {
                Text = x.paymentmethodname,
                Value = x.id.ToString()
            }).ToList();

            viewModel.ShippingMethods = context.Shippingmethods.ToList();

            viewModel.UserDetail = context.Users.FirstOrDefault(x => x.mail == HttpContext.User.Identity.Name);
            viewModel.UserId = viewModel.UserDetail.id;
            viewModel.BasketItems = context.Basket_items.Where(x => x.basketid == basketid).ToList();

            viewModel.Addresses = context.Addresses.Where(x => x.userid == viewModel.UserDetail.id).Select(x => new SelectListItem
            {
                Text = x.name,
                Value = x.id.ToString()
            }).ToList();

            return View(viewModel);
        }

        [Route("basket/checkout/{basketid}")]
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(CheckOutViewModel viewModel)
        {
            var basket = context.Baskets.Find(viewModel.BasketId);
            if (basket == null)
                return RedirectToAction("PageNotFound", "System");

            basket.billingaddressid = viewModel.SelectedBillingAddressId;
            basket.paymentmethodid = viewModel.SelectedPaymentMethodId;
            basket.shippingaddressid = viewModel.SelectedShippingAddressId;
            basket.shippingmethodid = viewModel.SelectedShippingMethodId;
            basket.userid = viewModel.UserId;

            context.Baskets.Attach(basket);
            context.Entry(basket).State = EntityState.Modified;
            context.SaveChanges();
            
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToBasket(BasketItem item)
        {
            try
            {
                var product = context.Products.FirstOrDefault(x => x.id == item.ProductId && x.isdeleted == false);
                if (product == null)
                {
                    return RedirectToAction("pagenotfound", "system");
                }

                if (product.stockquantity < item.ProductQuantity)
                {
                    TempData["Message"] = string.Format("{0} isim üründen istediğiniz adette yoktur. Maximum {1} adet alabilirsiniz!", product.name, product.stockquantity);
                    return RedirectToAction("Detail", "Product", new { productId = item.ProductId });
                }

                Basket basket = null;
                if (Request.Cookies.AllKeys.Contains("basketId"))
                {
                    if (!string.IsNullOrEmpty(Request.Cookies["basketId"].Value))
                    {
                        int basketId = int.Parse(Request.Cookies["basketId"].Value);
                        basket = context.Baskets.FirstOrDefault(x => x.id == basketId && x.billingaddressid == null) ?? new Dal.Entities.Basket();
                    }
                }
                else
                {
                    basket = context.Baskets.Create();
                    basket.createdtime = DateTime.Now;
                }

                basket.totalprice = basket.totalprice + (item.ProductQuantity * product.price);

                var basketItem = new Basket_item();
                basketItem.productid = item.ProductId;
                basketItem.quantity = item.ProductQuantity;
                basketItem.price = item.ProductQuantity * product.price;
                basketItem.basketid = basket.id;
                basketItem.addeddate = DateTime.Now;
                context.Basket_items.Add(basketItem);

                if (basket.id == 0)
                    context.Baskets.Add(basket);
                else
                    context.Baskets.Attach(basket);

                context.SaveChanges();

                HttpCookie cookie = new HttpCookie("basketId", basket.id.ToString());
                Response.Cookies.Add(cookie);

                TempData["Message"] = string.Format("{0} isimli ürün sepetinize eklenmiştir.", product.name);
                return RedirectToAction("Detail", "Product", new { productId = item.ProductId });
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Detail", "Product", new { productId = item.ProductId });
            }

        }
    }
}