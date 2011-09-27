using System.Collections.Generic;
using System.Web.Mvc;
using RentCar.Models;
using RentCar.Service;

namespace RentCar.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class MarcasController : Controller
    {
        private readonly RepositorioMarca repositorio = RepositorioFacade.Instance.RepositorioMarca;

        //
        // GET: /Marcas/

        public ActionResult Index()
        {
            var lista = repositorio.BuscarTodos();
            return View(lista);
        }

        //
        // GET: /Marcas/Details/5

        public ActionResult Details(int id)
        {
            var marca = repositorio.BuscarPorId(id);
            return View(marca);
        }

        //
        // GET: /Marcas/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Marcas/Create

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {

            var marca = new Marca();

            if (TryUpdateModel(marca))
            {
                repositorio.Salvar(marca);
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Marcas/Edit/5

        public ActionResult Edit(int id)
        {
            var marca = repositorio.BuscarPorId(id);
            return View(marca);
        }

        //
        // POST: /Marcas/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var marca = (Marca)repositorio.BuscarPorId(id);
                marca.Nome = collection.Get("Nome");

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Marcas/Delete/5

        public ActionResult Delete(int id)
        {
            var marca = repositorio.BuscarPorId(id);
            return View(marca);
        }

        //
        // POST: /Marcas/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                repositorio.Deletar(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
