using System.Collections.Generic;
using AngolaPrev.VivaEstetica.MVC.Models.Servico;

namespace AngolaPrev.VivaEstetica.MVC.Services.Serviços
{
    public interface IServicoService
    {
        void Cadastrar(CadastroServicoViewModel model);
        ObterServicoViewModel Get(int IdServico);
        IEnumerable<ObterServicoViewModel> ObterTodos();
    }
}