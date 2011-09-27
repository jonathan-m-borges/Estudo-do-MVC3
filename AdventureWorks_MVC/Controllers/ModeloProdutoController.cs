using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdventureWorks_MVC.Base.Controllers;
using AdventureWorks_MVC.Base.Service;
using AdventureWorks_MVC.Models;
using AdventureWorks_MVC.Service;

namespace AdventureWorks_MVC.Controllers
{
    public class ModeloProdutoController : CrudController<ModeloProduto>
    {

        protected override IRepositorio<ModeloProduto> Repositorio
        {
            get { return RepositorioFacade.Instance.RepositorioModeloProduto; }
        }

    }
}
