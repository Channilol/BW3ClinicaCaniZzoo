using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicaCaniZzoo.Models;

namespace ClinicaCaniZzoo.Controllers
{
    [Authorize (Roles = "AdminV")]
    public class HospitalizationsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Hospitalizations
        public ActionResult Index()
        {
            var hospitalizations = db.Hospitalizations.Include(h => h.Animals);
            return View(hospitalizations.ToList());
        }

        // GET: Hospitalizations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospitalizations hospitalizations = db.Hospitalizations.Find(id);
            if (hospitalizations == null)
            {
                return HttpNotFound();
            }
            return View(hospitalizations);
        }

        // GET: Hospitalizations/Create
        public ActionResult Create()
        {
            ViewBag.IdAnimale = new SelectList(db.Animals, "IdAnimale", "Nome");
            return View();
        }

        // POST: Hospitalizations/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRicovero,IdAnimale,StatoRicovero,FotoAnimale")] Hospitalizations hospitalizations)
        {
            if (ModelState.IsValid)
            {
                db.Hospitalizations.Add(hospitalizations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAnimale = new SelectList(db.Animals, "IdAnimale", "Nome", hospitalizations.IdAnimale);
            return View(hospitalizations);
        }

        // GET: Hospitalizations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospitalizations hospitalizations = db.Hospitalizations.Find(id);
            if (hospitalizations == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAnimale = new SelectList(db.Animals, "IdAnimale", "Nome", hospitalizations.IdAnimale);
            return View(hospitalizations);
        }

        // POST: Hospitalizations/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRicovero,IdAnimale,StatoRicovero,FotoAnimale")] Hospitalizations hospitalizations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hospitalizations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAnimale = new SelectList(db.Animals, "IdAnimale", "Nome", hospitalizations.IdAnimale);
            return View(hospitalizations);
        }

        // GET: Hospitalizations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospitalizations hospitalizations = db.Hospitalizations.Find(id);
            if (hospitalizations == null)
            {
                return HttpNotFound();
            }
            return View(hospitalizations);
        }

        // POST: Hospitalizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hospitalizations hospitalizations = db.Hospitalizations.Find(id);
            db.Hospitalizations.Remove(hospitalizations);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
