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
        public const string DataNaoDisponivel = "Data de agendamento não disponível.";
        public const string QuantidadeDeSessoesNaoPermitida = "Este serviço permite somente {0} seção(s).";
        public const string ServicoJaCadastrado = "Você ja cadastrou este serviço na data de {0}.";
        public const string NaoEPossivelCancelarAgendamento = "Não e possível mais cancelar este agendamento.";
        public const string DadosInvalidos = "Dados inválidos.";
        public const string CancelarVinteQuatroHorasAntecedencia = "Não e possível mais cancelar este agendamento. Massagens devem ser canceladas com 24 horas de antecedência.";
        public const string EditarServicoNaoExistente = "Não e possível editar um serviço que não exista.";
        public const string ProximoHorarioInvalido = "O horário {0} {1} ja está ocupado, procure outro horario com duas seções livres.";
    }
}