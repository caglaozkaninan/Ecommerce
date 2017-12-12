using ECommerce.Dal;
using ECommerce.Models;
using System.Linq;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        private ECommerceDbContext context = new ECommerceDbContext();

        [Route()]
        [Route("Home/Index/")]
        public ActionResult Index()
        {
            ProductListViewModel vm = new ProductListViewModel();
            vm.categories = context.Categories.Where(x => x.isdeleted == false).ToList();
            vm.product = context.Products.Where(x => x.isdeleted == false).ToList();
            vm.Basket = new Dal.Entities.Basket();

            if (Request.Cookies.AllKeys.Contains("basketId"))
            {
                if (!string.IsNullOrEmpty(Request.Cookies["basketId"].Value))
                {
                    int basketId = int.Parse(Request.Cookies["basketId"].Value);
                    vm.Basket = context.Baskets.FirstOrDefault(x=> x.id == basketId && x.billingaddressid == null) ?? new Dal.Entities.Basket();
                }
            }

            return View(vm);
        }
    }
}