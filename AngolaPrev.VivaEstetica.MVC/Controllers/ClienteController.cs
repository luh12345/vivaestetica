using AngolaPrev.VivaEstetica.MVC.Models.Clientes;
using AngolaPrev.VivaEstetica.MVC.Repository;
using AngolaPrev.VivaEstetica.MVC.Services.User;
using AngolaPrev.VivaEstetica.MVC.Services.User.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AngolaPrev.VivaEstetica.MVC.Controllers
{
    public class ClienteController : Controller
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
        public async Task<ActionResult> Cadastro(ClienteViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            AddUserResult result = await userService.AddUser(model);
            if (!result.IsSuccess)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", new Exception(error));

                return View(model);
            }

            return RedirectToAction("Index", "Login");
        }
    }
}