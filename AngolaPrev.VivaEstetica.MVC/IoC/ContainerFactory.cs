using AngolaPrev.VivaEstetica.MVC.Repository;
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

            builder.RegisterType<UserContext>().As<IUserContext>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            return builder.Build();
        }
    }
}