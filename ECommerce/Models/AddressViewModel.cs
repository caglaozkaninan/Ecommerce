using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Models
{
    public class AddressViewModel
    {
        public int AddressId { get; set; }

        [Required(ErrorMessage = "Adres Adı boş geçilemez")]
        public string name { get; set; }
        [Required(ErrorMessage ="Address boş geçilemez")]
        public string address1 { get; set; }

        [Required(ErrorMessage = "Address boş geçilemez")]
        public string address2 { get; set; }
        [Required(ErrorMessage = "Şehir boş geçilemez")]
        public string city { get; set; }
        [Required(ErrorMessage = "Posta Kodu boş geçilemez")]
        public int postcode { get; set; }

        [Required(ErrorMessage = "Ülke boş geçilemez")]
        public int countryid { get; set; }
        public IEnumerable<SelectListItem> Countrylist { get; set; }




    }
}