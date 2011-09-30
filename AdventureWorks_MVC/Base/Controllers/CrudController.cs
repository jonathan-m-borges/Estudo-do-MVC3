using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web.Mvc;
using AdventureWorks_MVC.Base.Binder;
using AdventureWorks_MVC.Base.Extensions;
using AdventureWorks_MVC.Base.Model;
using AdventureWorks_MVC.Base.Service;
using AdventureWorks_MVC.Models;
using System.Linq;

namespace AdventureWorks_MVC.Base.Controllers
{
    public abstract class CrudController<T> : Controller where T : Entidade, new()
    {
        protected abstract IRepositorio<T> Repositorio { get; }

        public Pagina Pagina = new Pagina(1, 10);

        public ViewResult Index()
        {
            return View(new ListaPaginada<T>(new List<T>(), Pagina));
        }

        public ActionResult Details(int id)
        {
            var entidade = Repositorio.BuscarPorId(id);
            return View(entidade);
        }

        public ActionResult Create()
        {
            var entidade = new T();
            return PartialView(entidade);
        }

        [HttpPost]
        public JsonResult Create(FormCollection collection)
        {
            try
            {
                var entidade = new T();
                UpdateModel(entidade);
                Repositorio.Salvar(entidade);
                return Json(new { result = true });
            }
            catch (Exception e)
            {
                return Json(new { result = false, msg = e.Message });
            }
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

        private Pagina MontarPaginacao(int? skip, int? top)
        {
            return new Pagina(skip != null ? skip.GetValueOrDefault() : Pagina.NumeroDaPagina,
                              top != null ? top.GetValueOrDefault() : Pagina.RegistrosPorPagina);
        }

        public JsonResult Lista(int? skip = null, int? top = null)
        {
            var listaPaginada = Repositorio.BuscarTodos(pagina: MontarPaginacao(skip, top));
            //var lista = MontarListaJson(listaPaginada);

            return CustonJson(listaPaginada, JsonRequestBehavior.AllowGet);
        }

        private List<object> MontarListaJson(ListaPaginada<T> listaPaginada)
        {
            var lista = new List<dynamic>();
            foreach (var item in listaPaginada.Lista)
            {

                dynamic objeto = new ObjetoDinamico();

                foreach (var prop in typeof(T).ObterModelMetadata().Properties.Where(x => x.PodeMostrar(ViewData)))
                    objeto.TrySetMember(prop.PropertyName, item.ObterValor(prop.PropertyName));

                lista.Add(objeto);
            }

            return lista;
        }

        private JsonResult CustonJson(object data, JsonRequestBehavior behavior)
        {
            return new CustomJsonResult
            {
                Data = data,
                JsonRequestBehavior = behavior
            };
        }
    }
}