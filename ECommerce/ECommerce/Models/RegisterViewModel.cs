using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class RegisterViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Kişi Adı boş geçilemez")]
        public string name { get; set; }

        [Required(ErrorMessage = "Kişi Soyadı boş geçilemez")]
        public string surname { get; set; }
        public string phone { get; set; }

        [Required(ErrorMessage = "Mail boş geçilemez")]
        public string mail { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez")]
        public string password { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez")]
        [Compare("password")]
        public string repassword { get; set; }


    }
}