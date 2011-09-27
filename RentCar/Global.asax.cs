using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.Routing;
using RentCar.Controllers;
using RentCar.Filters;
using RentCar.Models;
using RentCar.Service;
using RentCar.Validation;

namespace RentCar
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

        private void popularDados()
        {
            var repositorioMarcas = RepositorioFacade.Instance.RepositorioMarca;
            var repositorioCarros = RepositorioFacade.Instance.RepositorioCarro;

            repositorioMarcas.Salvar(new Marca { Nome = "GM" });
            repositorioMarcas.Salvar(new Marca { Nome = "Ford" });
            repositorioMarcas.Salvar(new Marca { Nome = "VolksWagen" });

            repositorioCarros.Salvar(new Carro
                                                    {
                                                        Cor = "Preta",
                                                        Marca = repositorioMarcas.BuscarPorId(3),
                                                        Modelo = "Gol",
                                                        Placa = "ABC-1234",
                                                        NumeroPortas = 4
                                                    });
            repositorioCarros.Salvar(new Carro
                                                    {
                                                        Cor = "Amarelo",
                                                        Marca = repositorioMarcas.BuscarPorId(2),
                                                        Modelo = "Fiesta",
                                                        Placa = "ABC-1232",
                                                        NumeroPortas = 4
                                                    });
            repositorioCarros.Salvar(new Carro
                                                    {
                                                        Cor = "Vermelho",
                                                        Marca = repositorioMarcas.BuscarPorId(3),
                                                        Modelo = "Fusca",
                                                        Placa = "ABC-1232",
                                                        NumeroPortas = 4
                                                    });
        }

        protected void Application_Start()
        {
            popularDados();
            AreaRegistration.RegisterAllAreas();

            GlobalFilters.Filters.Add(new InternationalizationFilter());

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}