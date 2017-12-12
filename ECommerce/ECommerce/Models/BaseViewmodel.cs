using ECommerce.Dal.Entities;
using System.Collections.Generic;

namespace ECommerce.Models
{
    public class BaseViewModel
    {
        public List<Category> categories { get; set; }

        public Basket Basket { get; set; }
    }
}