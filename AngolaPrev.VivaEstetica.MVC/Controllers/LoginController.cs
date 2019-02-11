using AngolaPrev.VivaEstetica.MVC.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngolaPrev.VivaEstetica.MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return RedirectToAction("Index", "Agenda");
        }
    }
}