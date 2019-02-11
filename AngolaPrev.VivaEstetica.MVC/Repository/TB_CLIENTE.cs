using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Repository
{
    public class TB_CLIENTE : IdentityUser
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public string Endereco { get; set; }
    }
}