using AngolaPrev.VivaEstetica.MVC.Models.Clientes;
using AngolaPrev.VivaEstetica.MVC.Models.Login;

namespace AngolaPrev.VivaEstetica.MVC.Services.User
{
    public interface IClienteService
    {
        void Registrar(ClienteViewModel model);
        void Login(LoginViewModel model);
        void EditarCliente(ObterClienteViewModel model);
        ObterClienteViewModel ObterCliente(int id);
        void Logout();
    }
}