using AngolaPrev.VivaEstetica.MVC.Models.Clientes;
using AngolaPrev.VivaEstetica.MVC.Repository;
using AngolaPrev.VivaEstetica.MVC.Services.User.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserContext userContext;

        public UserService(IUserContext userContext)
        {
            this.userContext = userContext;
        }

        public async Task<AddUserResult> AddUser(ClienteViewModel model)
        {
            TB_CLIENTE user = new TB_CLIENTE
            {
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Email = model.Email,
                Endereco = model.Endereco,
                Cpf = model.CPF,
                PhoneNumber = model.Telefone
            };

            IEnumerable<string> errors = await userContext.AddUser(user, model.Password);
            return new AddUserResult(!errors.Any(), errors);
        }
    }
}