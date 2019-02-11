using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Repository
{
    public class TB_SERVICOS
    {
        [Key]
        public int Id { get; set; }

        public string DS_SERVICO { get; set; }

    }
}