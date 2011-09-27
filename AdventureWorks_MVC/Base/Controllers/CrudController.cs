using System;
using System.Web.Mvc;
using AdventureWorks_MVC.Base.Binder;
using AdventureWorks_MVC.Base.Model;
using AdventureWorks_MVC.Base.Service;
using AdventureWorks_MVC.Models;

namespace AdventureWorks_MVC.Base.Controllers
{
    public abstract class CrudController<T> : Controller where T : Entidade, new()
    {
        protected abstract IRepositorio<T> Repositorio { get; }

        public Pagina Pagina = new Pagina(1, 10);

        public ViewResult Index(int? skip = null, int? top = null)
        {
            MontaPaginacao(skip, top);
            var listaPaginada = Repositorio.BuscarTodos(pagina: Pagina);
            return View(listaPaginada);
        }

        public ActionResult Details(int id)
        {
            var entidade = Repositorio.BuscarPorId(id);
            return View(entidade);
        }

        public ActionResult Create()
        {
            var entidade = new T();
            return View(entidade);
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var entidade = new T();
            if (TryUpdateModel(entidade))
            {
                Repositorio.Salvar(entidade);
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            var entidade = Repositorio.BuscarPorId(id);
            return View(entidade);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var entidade = Repositorio.BuscarPorId(id);

            try
            {
                UpdateModel(entidade);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            if (TryUpdateModel(entidade))
            {
                Repositorio.Salvar(entidade);
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Delete(int id)
        {
            var entidade = Repositorio.BuscarPorId(id);
            return View(entidade);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Repositorio.Excluir(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void MontaPaginacao(int? skip, int? top)
        {
            if (skip != null)
                Pagina.NumeroDaPagina = skip.GetValueOrDefault();
            if (top != null)
                Pagina.RegistrosPorPagina = top.GetValueOrDefault();
        }

    }
}