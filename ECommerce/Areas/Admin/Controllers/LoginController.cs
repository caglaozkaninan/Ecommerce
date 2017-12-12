using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Dal.Entities;
using System.Web.Security;
using ECommerce.Areas.Admin.Models;
using System.Text;
using System.Security.Cryptography;

namespace ECommerce.Areas.Admin.Controllers
{
    [RouteArea("Admin", AreaPrefix = "admin")]
    [RoutePrefix("login")]
    public class LoginController : Controller
    {
        ECommerce.Dal.ECommerceDbContext context = new Dal.ECommerceDbContext();

        [AllowAnonymous]
        [Route("login", Name = "AdminLogin")]
        public ActionResult Login(string ReturnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name) == false)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login");
            }
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login", Name = "AdminLoginPost")]
        public ActionResult Login(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                string md5sifre = Tools.GetMd5Hash(user.password);
                var loginuser = context.Users.FirstOrDefault(x => x.mail == user.username && x.password == md5sifre);
                if (loginuser == null)
                {
                    ViewBag.Message = "Hatalı";
                    return View(user);
                }
                else
                {
                    var authTicket = new FormsAuthenticationTicket(
                       1,                             // version
                       loginuser.mail, // user name
                       DateTime.Now,                  // created
                       DateTime.Now.AddMinutes(20),   // expires
                       false,                    // persistent?
                       loginuser.Role.rolename // can be used to store roles
                       );

                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);

                    if (loginuser.Role.rolename == "Admin")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else if (loginuser.Role.rolename == "User")
                    {
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }

                    ViewBag.Message = "Gerekli Kullancı Rolü Bulunamadı!!";
                    return View(user);
                }
            }

        }
        [Authorize]
        [Route("logoff", Name = "AdminLogoff")]
        public ActionResult LogOff(string returnUrl = null)
        {
            FormsAuthentication.SignOut();

            if (string.IsNullOrEmpty(returnUrl) == false)
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("login", "Login");
        }


    }
}