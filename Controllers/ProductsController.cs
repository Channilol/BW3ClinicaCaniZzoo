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
    [Authorize (Roles = "AdminF")]
    public class ProductsController : Controller
    {
        private DBContext db = new DBContext();

        public ActionResult IndexFarmacia()
        {
            var products = db.Products.Include(p => p.Suppliers).ToList();
            return View(products);
        }

        public ActionResult Search(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                var productSearch = db.Products
                                    .Where(p => p.NomeProdotto.Contains(query))
                                    .Select(p => new {
                                        IdProdotto = p.IdProdotto,
                                        NomeProdotto = p.NomeProdotto,
                                        ImgProdotto = p.ImgProdotto,
                                        TipoProdotto = p.TipoProdotto,
                                        Armadietto = p.Armadietto,
                                        Cassetto = p.Cassetto
                                    })
                                    .ToList();
                return Json(productSearch, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "La query è vuota" }, JsonRequestBehavior.AllowGet);
            }
        }


        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Suppliers);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        // Nel metodo Details del controller ProductsController
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }

            var clienti = db.Users.Where(r => r.Ruolo == "User")
                      .Select(r => new { IdCliente = r.IdUser, NomeCompleto = r.Nome + " " + r.Cognome + " " + r.CodiceFiscale })
                      .ToList();
            ViewBag.Clienti = new SelectList(clienti, "IdCliente", "NomeCompleto");

            return View(products);
        }



        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.IdFornitore = new SelectList(db.Suppliers, "IdFornitore", "Nome");
            return View();
        }

        // POST: Products/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProdotto,NomeProdotto,IdFornitore,TipoProdotto,UsiProdotto,ImgProdotto,Armadietto,Cassetto")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdFornitore = new SelectList(db.Suppliers, "IdFornitore", "Nome", products.IdFornitore);
            return View(products);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdFornitore = new SelectList(db.Suppliers, "IdFornitore", "Nome", products.IdFornitore);
            return View(products);
        }

        // POST: Products/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProdotto,NomeProdotto,IdFornitore,TipoProdotto,UsiProdotto,ImgProdotto,Armadietto,Cassetto")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdFornitore = new SelectList(db.Suppliers, "IdFornitore", "Nome", products.IdFornitore);
            return View(products);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Acquista(int IdCliente, DateTime DataVendita, int NumeroRicetta, int IdProdotto)
        {
            if (ModelState.IsValid)
            {
                Sales sale = new Sales
                {
                    IdUser = IdCliente,
                    DataVendita = DataVendita,
                    IdProdotto = IdProdotto
                };
                if (NumeroRicetta != 0)
                {
                    sale.N_Ricetta = NumeroRicetta;
                }
                db.Sales.Add(sale);
                db.SaveChanges();

                TempData["Message"] = "Acquisto completato con successo.";
                return RedirectToAction("IndexFarmacia", "Products");
            }
            TempData["Message"] = "Si è verificato un errore durante l'acquisto.";
            return View("Details");
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
