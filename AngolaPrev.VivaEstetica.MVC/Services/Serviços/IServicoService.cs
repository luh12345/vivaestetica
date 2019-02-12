using System.Collections.Generic;
using AngolaPrev.VivaEstetica.MVC.Models.Servico;

namespace AngolaPrev.VivaEstetica.MVC.Services.Serviços
{
    public interface IServicoService
    {
        void Add(CadastroServicoViewModel model);
        ObterServicoViewModel Get(int IdServico);
        IEnumerable<ObterServicoViewModel> GetAll();
    }
}