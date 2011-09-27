using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentCarEntity.Models;

namespace RentCarEntity.Controllers
{ 
    public class CarroController : Controller
    {
        private EntityModelContainer db = new EntityModelContainer();

        //
        // GET: /Carro/

        public ViewResult Index()
        {
            var carros = db.Carros.Include("Marca");
            return View(carros.ToList());
        }

        //
        // GET: /Carro/Details/5

        public ViewResult Details(int id)
        {
            Carro carro = db.Carros.Single(c => c.Id == id);
            return View(carro);
        }

        //
        // GET: /Carro/Create

        public ActionResult Create()
        {
            ViewBag.MarcaId = new SelectList(db.Marcas, "Id", "Nome");
            return View();
        } 

        //
        // POST: /Carro/Create

        [HttpPost]
        public ActionResult Create(Carro carro)
        {
            if (ModelState.IsValid)
            {
                db.Carros.AddObject(carro);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.MarcaId = new SelectList(db.Marcas, "Id", "Nome", carro.MarcaId);
            return View(carro);
        }
        
        //
        // GET: /Carro/Edit/5
 
        public ActionResult Edit(int id)
        {
            Carro carro = db.Carros.Single(c => c.Id == id);
            ViewBag.MarcaId = new SelectList(db.Marcas, "Id", "Nome", carro.MarcaId);
            return View(carro);
        }

        //
        // POST: /Carro/Edit/5

        [HttpPost]
        public ActionResult Edit(Carro carro)
        {
            if (ModelState.IsValid)
            {
                db.Carros.Attach(carro);
                db.ObjectStateManager.ChangeObjectState(carro, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MarcaId = new SelectList(db.Marcas, "Id", "Nome", carro.MarcaId);
            return View(carro);
        }

        //
        // GET: /Carro/Delete/5
 
        public ActionResult Delete(int id)
        {
            Carro carro = db.Carros.Single(c => c.Id == id);
            return View(carro);
        }

        //
        // POST: /Carro/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Carro carro = db.Carros.Single(c => c.Id == id);
            db.Carros.DeleteObject(carro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}