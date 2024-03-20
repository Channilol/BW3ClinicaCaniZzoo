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
    public class AnimalsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Animals
        public ActionResult Index()
        {
            var animals = db.Animals.Include(a => a.Users);
            return View(animals.ToList());
        }

        // GET: Animals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animals animals = db.Animals.Find(id);
            if (animals == null)
            {
                return HttpNotFound();
            }
            return View(animals);
        }

        // GET: Animals/Create
        public ActionResult Create()
        {
            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "Nome");
            return View();
        }

        // POST: Animals/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAnimale,DataRegistrazione,Nome,Tipologia,ColoreMantello,DataNascita,Microchip,IdUser")] Animals animals)
        {
            if (ModelState.IsValid)
            {
                db.Animals.Add(animals);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "Nome", animals.IdUser);
            return View(animals);
        }

        // GET: Animals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animals animals = db.Animals.Find(id);
            if (animals == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "Nome", animals.IdUser);
            return View(animals);
        }

        // POST: Animals/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAnimale,DataRegistrazione,Nome,Tipologia,ColoreMantello,DataNascita,Microchip,IdUser")] Animals animals)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animals).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "Nome", animals.IdUser);
            return View(animals);
        }

        // GET: Animals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animals animals = db.Animals.Find(id);
            if (animals == null)
            {
                return HttpNotFound();
            }
            return View(animals);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Animals animals = db.Animals.Find(id);
            db.Animals.Remove(animals);
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



        public ActionResult SearchAnimal(string id)
        {
            Animals animal = db.Animals.FirstOrDefault(a => a.Microchip == id);

            if (animal != null)
            {
                ViewBag.SearchedAnimal = animal;
            }
            else
            {
                ViewBag.SearchedAnimal = null; 
            }

            return View();
        }

    }
}
