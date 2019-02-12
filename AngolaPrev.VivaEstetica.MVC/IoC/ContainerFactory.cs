using AngolaPrev.VivaEstetica.MVC.Repository;
using AngolaPrev.VivaEstetica.MVC.Repository.Contract;
using AngolaPrev.VivaEstetica.MVC.Services.Agenda;
using AngolaPrev.VivaEstetica.MVC.Services.Serviços;
using AngolaPrev.VivaEstetica.MVC.Services.User;
using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.IoC
{
    public static class ContainerFactory
    {
        public static IContainer GetContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterFilterProvider();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            builder.RegisterType<ServicoService>().As<IServicoService>().InstancePerRequest();
            builder.RegisterType<AgendaService>().As<IAgendaService>().InstancePerRequest();
            builder.RegisterType<VivaEsteticaContext>().As<IVivaEsteticaContext>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());
            return builder.Build();
        }
    }
}