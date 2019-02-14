using AngolaPrev.VivaEstetica.MVC.Common.Const;
using AngolaPrev.VivaEstetica.MVC.Models.Clientes;
using AngolaPrev.VivaEstetica.MVC.Services.User;
using System;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace AngolaPrev.VivaEstetica.MVC.Controllers
{

    public class ClienteController : BaseController
    {
        private readonly IClienteService clienteService;

        public ClienteController(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }

        [Authorize]
        public ActionResult Perfil()
        {
            var model = clienteService.ObterCliente(GetUserId());
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public ActionResult EditarPerfil(ObterClienteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Callback(new Exception(ExceptionMessages.DadosInvalidos));
            }
            else
            {
                try
                {
                    clienteService.EditarCliente(model);
                    Callback();
                }
                catch (Exception ex)
                {
                    Callback(ex);
                }
            }

            return RedirectToAction("Perfil");
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
                clienteService.Registrar(model);
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