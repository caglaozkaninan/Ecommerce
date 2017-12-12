using ECommerce.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Areas.Admin.Models
{
    public class BrandViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
    }
}