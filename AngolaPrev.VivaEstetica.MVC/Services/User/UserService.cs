using AngolaPrev.VivaEstetica.MVC.Common.Const;
using AngolaPrev.VivaEstetica.MVC.Models.Clientes;
using AngolaPrev.VivaEstetica.MVC.Models.Login;
using System;
using WebMatrix.WebData;

namespace AngolaPrev.VivaEstetica.MVC.Services.User
{
    public class UserService : IUserService
    {
        public void Register(ClienteViewModel model)
        {
            try
            {
                WebSecurity.CreateUserAndAccount(model.Email, model.Password, new
                {
                    model.Nome,
                    model.Cpf,
                    model.Telefone,
                    model.Endereco
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.UsuarioJaCadastrado, ex);
            }
        }

        public void Login(LoginViewModel model)
        {
            if (WebSecurity.UserExists(model.Email))
            {
                if (!WebSecurity.Login(model.Email, model.Password))
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