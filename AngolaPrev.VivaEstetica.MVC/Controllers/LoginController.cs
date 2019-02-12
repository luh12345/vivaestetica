using AngolaPrev.VivaEstetica.MVC.Models.Login;
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
        private readonly IUserService userService;

        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                userService.Login(model);
                Callback($"Seja bem vindo ao sistema de agendamento da Vida Estetica.");
            }
            catch (Exception ex)
            {
                Callback(ex);
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            userService.Logout();
            return RedirectToAction("Login");
        }
    }
}