using AngolaPrev.VivaEstetica.MVC.Models.Clientes;
using AngolaPrev.VivaEstetica.MVC.Models.Login;

namespace AngolaPrev.VivaEstetica.MVC.Services.User
{
    public interface IClienteService
    {
        void CadastrarCliente(ClienteViewModel model);
        void EditarCliente(ObterClienteViewModel model);
        ObterClienteViewModel ObterCliente(int id);
    }
}