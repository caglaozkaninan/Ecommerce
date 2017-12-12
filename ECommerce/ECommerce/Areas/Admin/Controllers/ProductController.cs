using ECommerce.Areas.Admin.Models;
using ECommerce.Dal;
using ECommerce.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        // GET: Admin/Product
        ECommerce.Dal.ECommerceDbContext context = new Dal.ECommerceDbContext();
        public ActionResult Index()
        {
            List<Product> products = context.Products.Where(x => x.isdeleted == false).ToList();
            return View(products);
        }


        [HttpGet]
        public ActionResult ProductAdd()
        {
            AddProductViewModel viewModel = new AddProductViewModel();
            viewModel.Categorylist = context.Subcategories.Select(x => new SelectListItem()
            {
                Value = x.id.ToString(),
                Text = x.name
            }).ToList();

            viewModel.Brandlist = context.Brands.Select(x => new SelectListItem()
            {
                Value = x.id.ToString(),
                Text = x.name
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductAdd(AddProductViewModel model)
        {
            var imagePaths = SaveImages(model);

            if (ModelState.IsValid)
            {
                Product newProduct = new Product();
                newProduct.name = model.Name;
                newProduct.specification = model.Specification;
                newProduct.stockquantity = model.StockQuantity;
                newProduct.brandid = model.BrandId;
                newProduct.categoryid = model.CategoryId;
                newProduct.price = model.Price;
                newProduct.picturesmall = imagePaths[0];
                newProduct.picturebig = imagePaths[1];

                context.Products.Add(newProduct);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Delete(int productId)
        {
            try
            {
                var product = context.Products.Find(productId);
                product.isdeleted = true;
                context.Products.Attach(product);
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
                TempData["Status"] = true;
                TempData["Message"] = "Ürün sistemden başarıyla silindi";
            }
            catch (Exception ex)
            {
                TempData["Status"] = false;
                TempData["Message"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int productId)
        {
            var product = context.Products.Find(productId);
            if (product == null)
                return RedirectToAction("Index");

            var viewModel = new AddProductViewModel();
            viewModel.BrandId = product.brandid;
            viewModel.CategoryId = product.categoryid;
            viewModel.Name = product.name;
            viewModel.Price = product.price;
            viewModel.Specification = product.specification;
            viewModel.StockQuantity = product.stockquantity;

            viewModel.Categorylist = context.Subcategories.Select(x => new SelectListItem()
            {
                Value = x.id.ToString(),
                Text = x.name
            }).ToList();

            viewModel.Brandlist = context.Brands.Select(x => new SelectListItem()
            {
                Value = x.id.ToString(),
                Text = x.name
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddProductViewModel viewModel)
        {
            var imagePaths = SaveImages(viewModel);

            if (ModelState.IsValid)
            {
                Product newProduct = new Product();
                newProduct.id = viewModel.ProductId;
                newProduct.name = viewModel.Name;
                newProduct.specification = viewModel.Specification;
                newProduct.stockquantity = viewModel.StockQuantity;
                newProduct.brandid = viewModel.BrandId;
                newProduct.categoryid = viewModel.CategoryId;
                newProduct.price = viewModel.Price;
                newProduct.picturesmall = imagePaths[0];
                newProduct.picturebig = imagePaths[1];

                context.Products.Attach(newProduct);
                context.Entry(newProduct).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            viewModel.Categorylist = context.Subcategories.Select(x => new SelectListItem()
            {
                Value = x.id.ToString(),
                Text = x.name
            }).ToList();

            viewModel.Brandlist = context.Brands.Select(x => new SelectListItem()
            {
                Value = x.id.ToString(),
                Text = x.name
            }).ToList();

            return View(viewModel);
        }

        private string[] SaveImages(AddProductViewModel model)
        {
            var imagePaths = new List<string>();

            var validImageTypes = new string[]
           {
                "image/jpeg",
                "image/png"
           };

            if (model.BigPicture == null || model.BigPicture.ContentLength == 0)
            {
                ModelState.AddModelError("BigPicture", "BigPicture field is required");
            }
            else if (!validImageTypes.Contains(model.BigPicture.ContentType))
            {
                ModelState.AddModelError("BigPicture", "Please choose either a GIF, JPG or PNG image.");
            }

            if (model.SmallPicture == null || model.SmallPicture.ContentLength == 0)
            {
                ModelState.AddModelError("SmallPicture", "SmallPicture field is required");
            }
            else if (!validImageTypes.Contains(model.SmallPicture.ContentType))
            {
                ModelState.AddModelError("SmallPicture", "Please choose either a GIF, JPG or PNG image.");
            }

            if (ModelState.IsValid)
            {

                if (model.SmallPicture != null && model.SmallPicture.ContentLength > 0)
                {
                    var uploadDir = "/uploads";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.SmallPicture.FileName);
                    var imageUrl = Path.Combine(uploadDir, model.SmallPicture.FileName);
                    model.SmallPicture.SaveAs(imagePath);
                    imagePaths.Add(imageUrl);
                }

                if (model.BigPicture != null && model.BigPicture.ContentLength > 0)
                {
                    var uploadDir = "/uploads";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.BigPicture.FileName);
                    var imageUrl = Path.Combine(uploadDir, model.BigPicture.FileName);
                    model.BigPicture.SaveAs(imagePath);
                    imagePaths.Add(imageUrl);
                }

                return imagePaths.ToArray();
            }
            return null;
        }

    }

}