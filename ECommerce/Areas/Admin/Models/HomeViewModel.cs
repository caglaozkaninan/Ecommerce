using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Areas.Admin.Models
{
    public class HomeViewModel
    {
        public int ProductCount { get; set; }
        public int SiparisCount { get; set; }
        public int CustomerCount { get; set; }
    }
}