using ECommerce.Dal;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        private ECommerceDbContext context = new ECommerceDbContext();

        [Route("product/Detail/{productId?}")]
        public ActionResult Detail(int? productId)
        {
            if (productId.HasValue == false)
            {
                return RedirectToAction("pagenotfound", "system");
            }

            var product = context.Products.FirstOrDefault(x => x.id == productId);

            if (product == null)
            {
                return RedirectToAction("pagenotfound", "system");
            }

            ProductViewModel vm = new ProductViewModel();
            vm.categories = context.Categories.Where(x => x.isdeleted == false).ToList();
            vm.product = product;

            if (Request.Cookies.AllKeys.Contains("basketId"))
            {
                if (!string.IsNullOrEmpty(Request.Cookies["basketId"].Value))
                {
                    int basketId = int.Parse(Request.Cookies["basketId"].Value);
                    vm.Basket = context.Baskets.FirstOrDefault(x => x.id == basketId && x.billingaddressid == null) ?? new Dal.Entities.Basket();
                }
            }

            return View(vm);
        }

        [Route("product/Index/{subcategoryId?}")]
        public ActionResult Index(int? subcategoryId)
        {

            if (subcategoryId.HasValue == false)
            {
                return RedirectToAction("pagenotfound", "system");
            }

            ProductListViewModel vm = new ProductListViewModel();
            vm.categories = context.Categories.Where(x => x.isdeleted == false).ToList();
            vm.product = context.Products.Where(x => x.categoryid == subcategoryId.Value).ToList();


            if (Request.Cookies.AllKeys.Contains("basketId"))
            {
                if (!string.IsNullOrEmpty(Request.Cookies["basketId"].Value))
                {
                    int basketId = int.Parse(Request.Cookies["basketId"].Value);
                    vm.Basket = context.Baskets.FirstOrDefault(x => x.id == basketId && x.billingaddressid == null) ?? new Dal.Entities.Basket();
                }
            }

            if (vm.Basket == null)
                vm.Basket = new Dal.Entities.Basket();

            return View(vm);
        }

    }
}