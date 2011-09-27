using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentCar.Models;
using RentCar.Service;

namespace RentCar.Controllers
{
    [Authorize]
    public class AlugarController : Controller
    {

        private readonly RepositorioMarca repositorioMarcas = RepositorioFacade.Instance.RepositorioMarca;
        private readonly RepositorioCarro repositorioCarros = RepositorioFacade.Instance.RepositorioCarro;

        //
        // GET: /Locacao/

        public ActionResult Index()
        {
            ViewBag.Marcas = repositorioMarcas.BuscarTodos();
            ViewBag.Carros = new List<Carro>();
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            var locacao = new Locacao();
            TryUpdateModel(locacao);

            if (ModelState.IsValid)
            {
                var principal = HttpContext.User;
                return RedirectToAction("Index");
            }

            ViewBag.Marcas = repositorioMarcas.BuscarTodos();
            ViewBag.Carros = new List<Carro>();

            return View();
        }

        public JsonResult CarregarCarros(int idMarcas)
        {
            var carros = repositorioCarros.BuscarPorMarca(idMarcas);
            var jsonData = (from carro in carros select new { Id = carro.Id, Modelo = carro.Modelo }).ToArray();
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

    }
}
