using ECommerce.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        ECommerce.Dal.ECommerceDbContext context = new Dal.ECommerceDbContext();

        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "id,name,picture")] Category category, HttpPostedFileBase image1)
        {
            if (ModelState.IsValid)
            {
                context.Categories.Add(category);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }
        public ActionResult List()
        {
            List<Category> categories = context.Categories.Where(x => x.isdeleted == false).ToList();
            return View(categories);
        }
        public ActionResult Delete(int categoryId)
        {
            try
            {
                var category = context.Categories.Find(categoryId);
                category.isdeleted = true;
                context.Categories.Attach(category);
                context.Entry(category).State = EntityState.Modified;
                context.SaveChanges();
                TempData["Status"] = true;
                TempData["Message"] = "Kategori sistemden başarıyla silindi";
            }
            catch (Exception ex)
            {
                TempData["Status"] = false;
                TempData["Message"] = ex.Message;
            }
            return RedirectToAction("List");
        }
    }
}