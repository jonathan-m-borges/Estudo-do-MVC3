using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AdventureWorks_MVC.Base.Model;
using AdventureWorks_MVC.Base.Service;
using AdventureWorks_MVC.Models;
using AdventureWorks_MVC.Service;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;

namespace AdventureWorks_MVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            RepositorioDados.PopularDados();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            RegistrarWindsor();
        }

        private void RegistrarWindsor()
        {
            var container = new WindsorContainer();

            container.Register(Component.For<IRepositorio<CategoriaProduto>>().Instance(RepositorioFacade.Instance.RepositorioCategoriaProduto));
            container.Register(Component.For<IRepositorio<ModeloProduto>>().Instance(RepositorioFacade.Instance.RepositorioModeloProduto));

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }
    }
}