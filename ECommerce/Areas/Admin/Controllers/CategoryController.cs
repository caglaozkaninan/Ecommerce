using ECommerce.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Areas.Admin.Controllers
{
    [RouteArea("Admin", AreaPrefix = "admin")]
    [RoutePrefix("category")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        ECommerce.Dal.ECommerceDbContext context = new Dal.ECommerceDbContext();

        // GET: Admin/Category

        [Route("index", Name = "AddCategory")]
        public ActionResult Index()
        {
            return View();
        }
        [Route("index" , Name ="AddCategoryPost")]
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
        [Route("list", Name = "ListCategory")]
        public ActionResult List()
        {
            List<Category> categories = context.Categories.Where(x => x.isdeleted == false).ToList();
            return View(categories);
        }
        [Route("delete", Name = "DeleteCategory")]
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