using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Common.Const
{
    public class ExceptionMessages
    {
        public const string UsuarioJaCadastrado = "Usuário ja cadastrado.";
        public const string UsuarioNaoExiste = "Usuário não cadastrado.";
        public const string SenhaIncorreta = "Senha incorreta.";
        public const string DataMenorQueAtual = "Data de agendamento não disponível.";
        public const string QuantidadeDeSessoesNaoPermitida = "Este serviço permite somente {0} seção(s).";
        public const string ServicoJaCadastrado = "Você ja cadastrou este serviço na data de {0}.";
        public const string NaoEPossivelCancelarAgendamento = "Não e possível mais cancelar este agendamento.";
    }
}