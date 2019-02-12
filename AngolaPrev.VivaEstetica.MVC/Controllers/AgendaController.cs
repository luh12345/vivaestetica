using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngolaPrev.VivaEstetica.MVC.Controllers
{
    public class AgendaController : Controller
    {
        public AgendaController()
        {

        }
        // GET: Agenda
        public ActionResult Index(DateTime data)
        {
            return View();
        }
    }
}