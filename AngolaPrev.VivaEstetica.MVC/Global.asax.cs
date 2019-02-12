using AngolaPrev.VivaEstetica.MVC.IoC;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;

namespace AngolaPrev.VivaEstetica.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(ContainerFactory.GetContainer()));

            string contextName = ConfigurationManager.AppSettings["context.name"];
            string usersTable = ConfigurationManager.AppSettings["users.table"];
            WebSecurity.InitializeDatabaseConnection(contextName, usersTable, "Id", "Email", true);
        }
    }
}
