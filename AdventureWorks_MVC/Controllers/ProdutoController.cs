using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdventureWorks_MVC.Base.Service;
using AdventureWorks_MVC.Models;
using AdventureWorks_MVC.Service;

namespace AdventureWorks_MVC.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IRepositorio<ModeloProduto> repositorio;

        public ProdutoController()
        {
            repositorio = RepositorioFacade.Instance.RepositorioModeloProduto;
        }

        //
        // GET: /Produto/

        public ActionResult Index()
        {
            var listaPaginada = repositorio.BuscarTodos();
            return View(listaPaginada.Lista);
        }

        //
        // GET: /Produto/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Produto/Create

        public ActionResult Create()
        {
            return PartialView();
        }

        //
        // POST: /Produto/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Produto/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Produto/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Produto/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Produto/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
