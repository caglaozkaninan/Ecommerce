using ECommerce.Dal.Entities;
using System.Collections.Generic;

namespace ECommerce.Models
{
    public class ProductListViewModel : BaseViewModel
    {
        public List<Product> product { get; set; }
    }
}