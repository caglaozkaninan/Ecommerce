using ECommerce.Areas.Admin.Models;
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
    [RoutePrefix("subcategory")]
    [Authorize(Roles = "Admin")]
    public class SubCategoryController : Controller
    {
        ECommerce.Dal.ECommerceDbContext context = new Dal.ECommerceDbContext();
        [HttpGet]
        [Route("subcategoryadd", Name = "AddSubCategory")]
        public ActionResult Index()
        {
            AddProductViewModel viewModel = new AddProductViewModel();
            viewModel.Categorylist = context.Categories.Where(x=>x.isdeleted==false).Select(x => new SelectListItem()
            {
                Value = x.id.ToString(),
                Text = x.name
            }).ToList();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("subcategoryadd", Name = "AddSubCategoryPost")]
        public ActionResult Index(AddProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Subcategory newSubcategory = new Subcategory();
                newSubcategory.name = model.Name;
                newSubcategory.categoryid = model.CategoryId;
                context.Subcategories.Add(newSubcategory);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [Route("subcategorylist", Name = "ListSubCategoryPost")]
        public ActionResult List()
        {
            List<Subcategory> subcategories = context.Subcategories.Where(x => x.isdeleted == false).ToList();
            return View(subcategories);
        }
        [Route("subcategorydelete", Name = "DeleteSubCategoryPost")]
        public ActionResult Delete(int subcategoryId)
        {
            try
            {
                var subcategory = context.Subcategories.Find(subcategoryId);
                subcategory.isdeleted = true;
                context.Subcategories.Attach(subcategory);
                context.Entry(subcategory).State = EntityState.Modified;
                context.SaveChanges();
                TempData["Status"] = true;
                TempData["Message"] = "Ürün sistemden başarıyla silindi";
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