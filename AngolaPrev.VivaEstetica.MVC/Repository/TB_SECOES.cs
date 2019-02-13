using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Repository
{
    public class TB_SECOES
    {
        [Key]
        public int Id { get; set; }
        public string DS_IDENTIFICADOR { get; set; }
    }
}