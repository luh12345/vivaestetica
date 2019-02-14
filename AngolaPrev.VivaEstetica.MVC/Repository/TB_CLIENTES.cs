using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Repository
{
    public class TB_CLIENTES
    {
        [Key]
        public int Id { get; set; }
        public string DS_NOME { get; set; }
        public string DS_EMAIL { get; set; }
        public string CPF { get; set; }
        public string DS_ENDERECO { get; set; }
        public string NU_TELEFONE { get; set; }
    }
}