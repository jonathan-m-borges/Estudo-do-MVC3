using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdventureWorksMVCEF.Models;

namespace AdventureWorksMVCEF.Controllers
{ 
    public class ProductModelController : Controller
    {
        private AdventureWorksLTEntities db = new AdventureWorksLTEntities();

        //
        // GET: /ProductModel/

        public ViewResult Index()
        {
            return View(db.ProductModels.ToList());
        }

        //
        // GET: /ProductModel/Details/5

        public ViewResult Details(int id)
        {
            ProductModel productmodel = db.ProductModels.Single(p => p.ProductModelID == id);
            return View(productmodel);
        }

        //
        // GET: /ProductModel/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ProductModel/Create

        [HttpPost]
        public ActionResult Create(ProductModel productmodel)
        {
            if (ModelState.IsValid)
            {
                db.ProductModels.AddObject(productmodel);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(productmodel);
        }
        
        //
        // GET: /ProductModel/Edit/5
 
        public ActionResult Edit(int id)
        {
            ProductModel productmodel = db.ProductModels.Single(p => p.ProductModelID == id);
            return View(productmodel);
        }

        //
        // POST: /ProductModel/Edit/5

        [HttpPost]
        public ActionResult Edit(ProductModel productmodel)
        {
            if (ModelState.IsValid)
            {
                db.ProductModels.Attach(productmodel);
                db.ObjectStateManager.ChangeObjectState(productmodel, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productmodel);
        }

        //
        // GET: /ProductModel/Delete/5
 
        public ActionResult Delete(int id)
        {
            ProductModel productmodel = db.ProductModels.Single(p => p.ProductModelID == id);
            return View(productmodel);
        }

        //
        // POST: /ProductModel/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ProductModel productmodel = db.ProductModels.Single(p => p.ProductModelID == id);
            db.ProductModels.DeleteObject(productmodel);
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