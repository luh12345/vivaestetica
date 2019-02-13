﻿using System;
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
        public int QT_SESSOES { get; set; }
        public int TP_MINUTOS { get; set; }
        public bool BT_MASSAGEM { get; set; }
    }
}