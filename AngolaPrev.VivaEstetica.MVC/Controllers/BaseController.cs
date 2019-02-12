using AngolaPrev.VivaEstetica.MVC.Common.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace AngolaPrev.VivaEstetica.MVC.Controllers
{
    public abstract class BaseController : Controller
    {
        public void Callback(string message = null)
        {
            if (string.IsNullOrEmpty(message))
                message = Messages.OperacaoRealizada;

            TempData["Callback"] = new KeyValuePair<string, string>("success", message);
        }

        public void Callback(Exception ex)
        {
            TempData["Callback"] = new KeyValuePair<string, string>("error", ex.Message);
        }

        public int GetUserId()
        {
            return WebSecurity.CurrentUserId;
        }
    }
}