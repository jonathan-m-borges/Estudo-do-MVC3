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
    public class MarcaController : Controller
    {
        private EntityModelContainer db = new EntityModelContainer();

        //
        // GET: /Marca/

        public ViewResult Index()
        {
            return View(db.Marcas.ToList());
        }

        //
        // GET: /Marca/Details/5

        public ViewResult Details(int id)
        {
            Marca marca = db.Marcas.Single(m => m.Id == id);
            return View(marca);
        }

        //
        // GET: /Marca/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Marca/Create

        [HttpPost]
        public ActionResult Create(Marca marca)
        {
            if (ModelState.IsValid)
            {
                db.Marcas.AddObject(marca);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(marca);
        }
        
        //
        // GET: /Marca/Edit/5
 
        public ActionResult Edit(int id)
        {
            Marca marca = db.Marcas.Single(m => m.Id == id);
            return View(marca);
        }

        //
        // POST: /Marca/Edit/5

        [HttpPost]
        public ActionResult Edit(Marca marca)
        {
            if (ModelState.IsValid)
            {
                db.Marcas.Attach(marca);
                db.ObjectStateManager.ChangeObjectState(marca, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marca);
        }

        //
        // GET: /Marca/Delete/5
 
        public ActionResult Delete(int id)
        {
            Marca marca = db.Marcas.Single(m => m.Id == id);
            return View(marca);
        }

        //
        // POST: /Marca/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Marca marca = db.Marcas.Single(m => m.Id == id);
            db.Marcas.DeleteObject(marca);
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