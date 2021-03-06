﻿using AngolaPrev.VivaEstetica.MVC.Common.Const;
using AngolaPrev.VivaEstetica.MVC.Models.Clientes;
using AngolaPrev.VivaEstetica.MVC.Models.Login;
using AngolaPrev.VivaEstetica.MVC.Repository;
using AngolaPrev.VivaEstetica.MVC.Repository.Contract;
using System;
using System.Linq;
using WebMatrix.WebData;

namespace AngolaPrev.VivaEstetica.MVC.Services.User
{
    public class ClienteService : IClienteService
    {
        private readonly IVivaEsteticaContext context;

        public ClienteService(IVivaEsteticaContext context)
        {
            this.context = context;
        }

        public void CadastrarCliente(ClienteViewModel model)
        {
            try
            {
                WebSecurity.CreateUserAndAccount(model.Email, model.Password, new
                {
                    DS_NOME = model.Nome,
                    CPF = model.Cpf,
                    NU_TELEFONE = model.Telefone,
                    DS_ENDERECO = model.Endereco
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.UsuarioJaCadastrado, ex);
            }
        }

        public ObterClienteViewModel ObterCliente(int id)
        {
            TB_CLIENTES cliente = context.TB_CLIENTES.Single(x => x.Id == id);

            return new ObterClienteViewModel
            {
                Id = id,
                Cpf = cliente.CPF,
                Email = cliente.DS_EMAIL,
                Endereco = cliente.DS_ENDERECO,
                Nome = cliente.DS_NOME,
                Telefone = cliente.NU_TELEFONE
            };
        }

        public void EditarCliente(ObterClienteViewModel model)
        {
            TB_CLIENTES cliente = context.TB_CLIENTES.Single(x => x.Id == model.Id);

            cliente.DS_NOME = model.Nome;
            cliente.NU_TELEFONE = model.Telefone;
            cliente.DS_ENDERECO = model.Endereco;
            cliente.CPF = model.Cpf;

            context.SaveChanges();
        }
    }
}