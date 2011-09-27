using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentCar.Models;
using RentCar.Service;

namespace RentCar.Controllers
{
    [Authorize(Roles = "Administrador")]

    public class CarrosController : Controller
    {




        private readonly RepositorioCarro repositorio = RepositorioFacade.Instance.RepositorioCarro;
        private readonly RepositorioMarca repositorioMarca = RepositorioFacade.Instance.RepositorioMarca;

        //
        // GET: /Carros/

        public ActionResult Index()
        {
            var lista = repositorio.BuscarTodos();
            return View("Index", lista);
        }

        //
        // GET: /Carros/Details/5

        public ActionResult Details(int id)
        {
            return View(repositorio.BuscarPorId(id));
        }

        //
        // GET: /Carros/Create

        public ActionResult Create()
        {
            ViewBag.Marca_List = repositorioMarca.BuscarTodos();
            return View();
        }

        protected bool TryUpdateModel(Entidade model)
        {
            return base.TryUpdateModel(model);
        }

        //
        // POST: /Carros/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var model = new Carro();

            if (TryUpdateModel(model))
            {
                UpdateMarca(collection, model);

                repositorio.Salvar(model);
                return Redirect("Index");
            }

            return Create();
        }

        private void UpdateMarca(FormCollection collection, Carro model)
        {
            var idMarca = Convert.ToInt32(collection.Get("Marca"));
            var marca = repositorioMarca.BuscarPorId(idMarca);
            model.Marca = marca;
        }

        //
        // GET: /Carros/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.Marca_List = repositorioMarca.BuscarTodos();
            return View(repositorio.BuscarPorId(id));
        }

        //
        // POST: /Carros/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var model = repositorio.BuscarPorId(id);
            if (TryUpdateModel(model))
            {
                UpdateMarca(collection, model);

                return RedirectToAction("Index");
            }

            return Edit(id);
        }

        //
        // GET: /Carros/Delete/5

        public ActionResult Delete(int id)
        {
            return View(repositorio.BuscarPorId(id));
        }

        //
        // POST: /Carros/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                repositorio.Deletar(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult ValidarPlaca(string placa, string Id)
        {
            var idCarro = 0;
            try
            {
                idCarro = Convert.ToInt32(Id);
            }
            catch
            {
            }
            return repositorio.BuscarPorFiltro(x => x.Placa == placa && x.Id != idCarro).Count > 0
                       ? Json(MyResources.Resource.Carro_ErroPlaca, JsonRequestBehavior.AllowGet)
                       : Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
