using ECommerce.Dal.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Areas.Admin.Models
{
    public class AddProductViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Ürün adı boş geçilemez")]
        public string Name { get; set; }

        public string Specification { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public HttpPostedFileBase BigPicture { get; set; }
        public HttpPostedFileBase SmallPicture { get; set; }

        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categorylist { get; set; }
        public int BrandId { get; set; }
        public IEnumerable<SelectListItem> Brandlist { get; set; }
    }
}