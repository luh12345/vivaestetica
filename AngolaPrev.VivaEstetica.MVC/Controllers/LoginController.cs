using AngolaPrev.VivaEstetica.MVC.Models.Login;
using AngolaPrev.VivaEstetica.MVC.Services.Login;
using AngolaPrev.VivaEstetica.MVC.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngolaPrev.VivaEstetica.MVC.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ILoginService loginService;

        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Agenda");

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                loginService.Login(model);
                Callback($"Seja bem vindo ao sistema de agendamento da Vida Estetica.");
            }
            catch (Exception ex)
            {
                Callback(ex);
                return View(model);
            }

            return RedirectToAction("Index", "Agenda");
        }

        public ActionResult Logout()
        {
            loginService.Logout();
            return RedirectToAction("Login");
        }
    }
}