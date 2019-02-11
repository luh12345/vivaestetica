using System.Collections.Generic;
using System.Threading.Tasks;
using AngolaPrev.VivaEstetica.MVC.Models.Clientes;
using AngolaPrev.VivaEstetica.MVC.Services.User.Result;

namespace AngolaPrev.VivaEstetica.MVC.Services.User
{
    public interface IUserService
    {
        Task<AddUserResult> AddUser(ClienteViewModel model);
    }
}