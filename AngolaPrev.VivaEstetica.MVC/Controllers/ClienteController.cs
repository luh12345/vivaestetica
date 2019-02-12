using AngolaPrev.VivaEstetica.MVC.Models.Clientes;
using AngolaPrev.VivaEstetica.MVC.Services.User;
using System;
using System.Web.Mvc;

namespace AngolaPrev.VivaEstetica.MVC.Controllers
{
    public class ClienteController : BaseController
    {
        private readonly IUserService userService;

        public ClienteController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Cadastro(ClienteViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                userService.Register(model);
                Callback();
            }
            catch (Exception ex)
            {
                Callback(ex);
                return View(model);
            }

            return RedirectToAction("Login", "Login");
        }
    }
}