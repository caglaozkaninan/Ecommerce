using ECommerce.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        ECommerce.Dal.ECommerceDbContext context = new Dal.ECommerceDbContext();
        // GET: Admin/User
        //[Route("Admin/User/Index")]
        public ActionResult Index()
        {
            List<User> users = context.Users.Where(x => x.roleid == 2).ToList();
            return View(users);
        }
    }
}