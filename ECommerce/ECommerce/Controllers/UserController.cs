using ECommerce.Dal;
using ECommerce.Dal.Entities;
using ECommerce.Models;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class UserController : Controller
    {
        private ECommerceDbContext context = new ECommerceDbContext();

        [Route("User/Register")]
        public ActionResult Register()
        {
            RegisterViewModel viewModel = new RegisterViewModel();
            return View(viewModel);
        }

        [Route("User/Register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string md5sifre = Tools.GetMd5Hash(model.password);                         
                User newUser = new User();
                newUser.roleid = 2;
                newUser.name = model.name;
                newUser.surname = model.surname;
                newUser.phone = model.phone;
                newUser.mail = model.mail;
                newUser.password = md5sifre;
                context.Users.Add(newUser);
                context.SaveChanges();
                return RedirectToAction("Login","Login",new { Area="Admin"});
            }

            return View(model);
        }
       
    }
}