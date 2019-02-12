using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Models.Servico
{
    public class CadastroServicoViewModel
    {
        [Required, Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required, IntegerValidator(MinValue = 1)]
        public int TotalSessoes { get; set; }

        [Required, IntegerValidator(MinValue = 30)]
        public int DuracaoMinutos { get; set; }
    }
}