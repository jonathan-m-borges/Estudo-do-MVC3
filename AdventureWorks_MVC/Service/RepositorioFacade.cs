using System;
using AdventureWorks_MVC.Base.Model;
using AdventureWorks_MVC.Base.Service;
using AdventureWorks_MVC.Models;

namespace AdventureWorks_MVC.Service
{
    public class RepositorioFacade
    {
        private static RepositorioFacade instance;

        public IRepositorio<CategoriaProduto> RepositorioCategoriaProduto { get; private set; }
        public IRepositorio<ModeloProduto> RepositorioModeloProduto { get; private set; }

        private RepositorioFacade()
        {
            RepositorioCategoriaProduto = new Repositorio<CategoriaProduto>();
            RepositorioModeloProduto = new Repositorio<ModeloProduto>();
        }

        public static RepositorioFacade Instance
        {
            get
            {
                return instance ?? (instance = new RepositorioFacade());
            }
        }

    }
}