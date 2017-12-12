using ECommerce.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Areas.Admin.Controllers
{
    [RouteArea("Admin", AreaPrefix = "admin")]
    [RoutePrefix("home")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        ECommerce.Dal.ECommerceDbContext context = new ECommerce.Dal.ECommerceDbContext();
        
        [Route]
        [Route("index" , Name ="HomePost")]
        public ActionResult Index()
        {
            HomeViewModel viewmodel = new HomeViewModel();
            viewmodel.ProductCount = context.Products.Where(x => x.isdeleted == false).Count();
            viewmodel.SiparisCount = context.Baskets.Count();
            viewmodel.CustomerCount=context.Users.Count();
            return View(viewmodel);
        }

    }
}