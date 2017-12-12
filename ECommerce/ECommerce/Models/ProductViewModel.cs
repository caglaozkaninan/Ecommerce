using ECommerce.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class ProductViewModel : BaseViewModel
    {
        public ProductViewModel()
        {
            Basket = new Basket();
        }
        public Product product { get; set; }
    }
}