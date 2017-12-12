using ECommerce.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Areas.Admin.Controllers
{
    [RouteArea("Admin", AreaPrefix = "admin")]
    [RoutePrefix("user")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        ECommerce.Dal.ECommerceDbContext context = new Dal.ECommerceDbContext();
        // GET: Admin/User
        //[Route("Admin/User/Index")]
        [Route("userlist", Name = "ListUser")]
        public ActionResult Index()
        {
            List<User> users = context.Users.Where(x => x.roleid == 2).ToList();
            return View(users);
        }
    }
}