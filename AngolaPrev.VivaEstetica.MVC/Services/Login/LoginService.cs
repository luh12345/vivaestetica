using AngolaPrev.VivaEstetica.MVC.Common.Const;
using AngolaPrev.VivaEstetica.MVC.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace AngolaPrev.VivaEstetica.MVC.Services.Login
{
    public class LoginService : ILoginService
    {
        public void Login(LoginViewModel model)
        {
            if (WebSecurity.UserExists(model.Email))
            {
                if (!WebSecurity.Login(model.Email, model.Password, model.ManterLogado))
                {
                    throw new Exception(ExceptionMessages.SenhaIncorreta);
                }
            }
            else
            {
                throw new Exception(ExceptionMessages.UsuarioNaoExiste);
            }
        }

        public void Logout()
        {
            WebSecurity.Logout();
        }
    }
}