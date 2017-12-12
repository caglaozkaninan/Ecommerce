using ECommerce.Dal.Entities;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    [Authorize(Roles = "User")]
    public class AddressesManagementController : Controller
    {
        ECommerce.Dal.ECommerceDbContext context = new ECommerce.Dal.ECommerceDbContext();

        [Route("User/Addresess", Name = "UserAddressList")]
        public ActionResult Index()
        {
            List<Address> addresses = context.Addresses.Where(x => x.User.mail == HttpContext.User.Identity.Name && x.isdeleted == false).ToList();
            return View(addresses);

        }


        [Route("User/Address/Add", Name = "UserNewAddress")]
        [HttpGet]
        public ActionResult NewAddress()
        {
            AddressViewModel viewmodel = new AddressViewModel();
            viewmodel.Countrylist = context.Countries.Select(x => new SelectListItem()
            {
                Value = x.id.ToString(),
                Text = x.name
            }).ToList();

            return View(viewmodel);
        }
        [Route("User/Address/Add", Name = "UserAddressAddPost")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewAddress(AddressViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Address address = new Address();
                    address.name = model.name;
                    address.address1 = model.address1;
                    address.address2 = model.address2;
                    address.city = model.city;
                    address.postcode = model.postcode;
                    address.countryid = model.countryid;

                    context.Addresses.Add(address);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            catch (Exception )
            {
                
            }
            return View(model);
        }
        [Route("User/Address/Edit", Name = "UserAddressEdit")]
        public ActionResult Edit(int addressId)
        {
            var address = context.Addresses.FirstOrDefault(x => x.User.mail == HttpContext.User.Identity.Name && x.id == addressId);
            if (address == null)
                return RedirectToAction("Index");

            var viewmodel = new AddressViewModel();
            viewmodel.countryid = address.countryid;
            viewmodel.name = address.name;
            viewmodel.address1 = address.address1;
            viewmodel.address2 = address.address2;
            viewmodel.city = address.city;
            viewmodel.postcode = address.postcode;
            viewmodel.Countrylist = context.Countries.Select(x => new SelectListItem()
            {
                Value = x.id.ToString(),
                Text = x.name
            }).ToList();

            return View(viewmodel);
        }

        [Route("User/Address/Edit", Name = "UserAddressEditPost")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddressViewModel ViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var address = context.Addresses.FirstOrDefault(x => x.User.mail == HttpContext.User.Identity.Name && x.id == ViewModel.AddressId);
                    address.name = ViewModel.name;
                    address.city = ViewModel.city;
                    address.postcode = ViewModel.postcode;
                    address.address1 = ViewModel.address1;
                    address.address2 = ViewModel.address2;
                    address.countryid = ViewModel.countryid;

                    context.Addresses.Attach(address);
                    context.Entry(address).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception)
            {

            }
            return View(ViewModel);

        }
        [Route("User/Address/Delete", Name = "UserAddressDelete")]
        public ActionResult Delete(int addressId)
        {
            try
            {
                var address = context.Addresses.FirstOrDefault(x => x.User.mail == HttpContext.User.Identity.Name && x.id == addressId);
                address.isdeleted = true;
                context.Addresses.Attach(address);
                context.Entry(address).State = EntityState.Modified;
                context.SaveChanges();
                TempData["Status"] = true;
                TempData["Message"] = "Marka sistemden başarıyla silindi";
            }
            catch (Exception ex)
            {
                TempData["Status"] = false;
                TempData["Message"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

    }
}