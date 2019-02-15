using AngolaPrev.VivaEstetica.MVC.Common.Const;
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

        public void Cadastrar(CadastroServicoViewModel model)
        {
            context.TB_SERVICOS.Add(new TB_SERVICOS
            {
                DS_SERVICO = model.Descricao,
                QT_SESSOES = model.TotalSessoes,
                TP_MINUTOS = model.DuracaoMinutos,
                BT_MASSAGEM = model.EhMassagem
            });

            context.SaveChanges();
        }

        public IEnumerable<ObterServicoViewModel> ObterTodos()
        {
            return context.TB_SERVICOS.ToArray().Select(x => new ObterServicoViewModel
            {
                Descricao = x.DS_SERVICO,
                IdServico = x.Id,
                Duracao = x.TP_MINUTOS,
                TotalSecoes = x.QT_SESSOES,
                EhMassagem = x.BT_MASSAGEM
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
                TotalSecoes = servico.QT_SESSOES
            };
        }

        public void Editar(ObterServicoViewModel model)
        {
            TB_SERVICOS servico = context.TB_SERVICOS.Single(x => x.Id == model.IdServico);

            if (servico == null)
                throw new Exception(ExceptionMessages.EditarServicoNaoExistente);

            servico.DS_SERVICO = model.Descricao;
            servico.QT_SESSOES = model.TotalSecoes;
            servico.BT_MASSAGEM = model.EhMassagem;
            servico.TP_MINUTOS = model.Duracao;

            context.SaveChanges();
        }

        public void Deletar(int idServico)
        {
            TB_SERVICOS servico = context.TB_SERVICOS.Single(x => x.Id == idServico);

            if (servico == null)
                throw new Exception(ExceptionMessages.EditarServicoNaoExistente);

            context.TB_SERVICOS.Remove(servico);
            context.SaveChanges();
        }
    }
}