using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Models.Servico
{
    public class ObterServicoViewModel
    {
        public int IdServico { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Total de seções")]
        public int TotalSecoes { get; set; }
        [Display(Name = "Duração")]
        public int Duracao { get; set; }
        [Display(Name = "É massagem?")]
        public bool EhMassagem { get; set; }
    }
}