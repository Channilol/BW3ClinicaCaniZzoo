using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using ClinicaCaniZzoo.Models;

namespace ClinicaCaniZzoo.Controllers
{
    [Authorize(Roles = "AdminV")]
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


        public ActionResult SearchAnimal()
        {
            return View("~/Views/Animals/SearchAnimal.cshtml");
        }




        [HttpPost]
        public ActionResult SearchAnimal(string id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DBContext"].ConnectionString;

            if (id != null)
            {
                string query = "SELECT a.Nome AS NomeAnimale, a.Tipologia, a.ColoreMantello, a.DataRegistrazione, a.DataNascita, u.Nome AS NomePadrone, u.Cognome AS CognomePadrone " +
                               "FROM Animals AS a " +
                               "LEFT JOIN Users AS u ON a.IdUser = u.IdUser " +
                               "WHERE a.Microchip = @Microchip ";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Microchip", id);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            var animal = new AnimalViewModel
                            {
                                Nome = reader["NomeAnimale"].ToString(),
                                Tipologia = reader["Tipologia"].ToString(),
                                ColoreMantello = reader["ColoreMantello"].ToString(),
                                DataRegistrazione = DateTime.Parse(reader["DataRegistrazione"].ToString()).ToShortDateString(),
                                DataNascita = DateTime.Parse(reader["DataNascita"].ToString()).ToShortDateString(),
                                NomePadrone = (reader["NomePadrone"] != DBNull.Value) ? reader["NomePadrone"].ToString() : "Senza Padrone",
                                CognomePadrone = (reader["CognomePadrone"] != DBNull.Value) ? reader["CognomePadrone"].ToString() : null,
                            };

                            ViewBag.SearchedAnimal = animal;
                        }
                        reader.Close();
                    }
                }
            }
            return View();
        }


    }
}
