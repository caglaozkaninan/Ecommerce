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
    public class BrandController : Controller
    {
        ECommerce.Dal.ECommerceDbContext context = new Dal.ECommerceDbContext();
        // GET: Admin/Brand
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "id,name,picture")] Brand brand, HttpPostedFileBase image1)
        {
            if (ModelState.IsValid)
            {
               context.Brands.Add(brand);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brand);
        }
        public ActionResult List()
        {
            List<Brand> brands = context.Brands.Where(x => x.isdeleted == false).ToList();
            return View(brands);
        }
        public ActionResult Delete(int brandId)
        {
            try
            {
                var brand = context.Brands.Find(brandId);
                brand.isdeleted = true;
                context.Brands.Attach(brand);
                context.Entry(brand).State = EntityState.Modified;
                context.SaveChanges();
                TempData["Status"] = true;
                TempData["Message"] = "Marka sistemden başarıyla silindi";
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
