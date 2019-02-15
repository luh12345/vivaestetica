using System.Collections.Generic;
using AngolaPrev.VivaEstetica.MVC.Models.Servico;

namespace AngolaPrev.VivaEstetica.MVC.Services.Serviços
{
    public interface IServicoService
    {
        void CadastrarServico(CadastroServicoViewModel model);
        ObterServicoViewModel ObterServico(int IdServico);
        IEnumerable<ObterServicoViewModel> ObterTodos();
        void EditarServico(ObterServicoViewModel model);
        void DeletarServico(int idServico);
    }
}