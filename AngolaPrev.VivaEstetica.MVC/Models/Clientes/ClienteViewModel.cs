using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Models.Clientes
{
    public class ClienteViewModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        [Required, EmailAddress, Display(Name = "E-mail")]
        public string Email { get; set; }
        [Required, Display(Name = "Senha")]
        public string Password { get; set; }
        [Required, Compare("Password"), Display(Name = "Confirmar senha")]
        public string ConfirmPassword { get; set; }
    }
}