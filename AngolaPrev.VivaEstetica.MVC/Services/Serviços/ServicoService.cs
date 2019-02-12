using AngolaPrev.VivaEstetica.MVC.Models.Servico;
using AngolaPrev.VivaEstetica.MVC.Repository;
using AngolaPrev.VivaEstetica.MVC.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Services.Serviços
{
    public class ServicoService : IServicoService
    {
        private readonly IVivaEsteticaContext context;

        public ServicoService(IVivaEsteticaContext context)
        {
            this.context = context;
        }

        public void Add(CadastroServicoViewModel model)
        {
            context.TB_SERVICOS.Add(new TB_SERVICOS
            {
                DS_SERVICO = model.Descricao,
                QT_SESSOES = model.TotalSessoes,
                TP_MINUTOS = model.DuracaoMinutos
            });

            context.SaveChanges();
        }

        public IEnumerable<ObterServicoViewModel> GetAll()
        {
            return context.TB_SERVICOS.ToArray().Select(x => new ObterServicoViewModel
            {
                Descricao = x.DS_SERVICO,
                IdServico = x.Id,
                Duracao = x.TP_MINUTOS,
                TotalSessoes = x.QT_SESSOES
            });
        }

        public ObterServicoViewModel Get(int IdServico)
        {
            TB_SERVICOS servico = context.TB_SERVICOS.Single(x => x.Id == IdServico);
            return new ObterServicoViewModel
            {
                Descricao = servico.DS_SERVICO,
                Duracao = servico.TP_MINUTOS,
                IdServico = servico.Id,
                TotalSessoes = servico.QT_SESSOES
            };
        }
    }
}