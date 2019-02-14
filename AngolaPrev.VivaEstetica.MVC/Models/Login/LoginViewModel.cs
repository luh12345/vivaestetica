using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Models.Login
{
    public class LoginViewModel
    {
        [Required, EmailAddress, Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required, Display(Name = "Senha")]
        public string Password { get; set; }
    }
}