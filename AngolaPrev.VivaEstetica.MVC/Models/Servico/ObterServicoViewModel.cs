using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Models.Servico
{
    public class ObterServicoViewModel
    {
        public int IdServico { get; set; }
        public string Descricao { get; set; }
        public int TotalSessoes { get; set; }
        public int Duracao { get; set; }
    }
}