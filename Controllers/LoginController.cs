﻿using ClinicaCaniZzoo.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ClinicaCaniZzoo.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            if (HttpContext.User.Identity.IsAuthenticated) // se sei già loggato torna indietro
            {
                TempData["Fail"] = "You must log out before you can sign up a new account";
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "Nome, Cognome, CodiceFiscale, Telefono, Email, Password, Ruolo")] Users newUser)
        {
            using (var db = new DBContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Users.Add(newUser);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return View(ex.Message);
                    }
                    TempData["Success"] = "You have signed up for a new account";
                    return RedirectToAction("Index", "Login");
                }
                return View();
            }

        }

        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated) // se sei già loggato torna indietro
            {
                TempData["Fail"] = "You must log out before you can log in with another account.";
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            using(var db = new DBContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {   //query select che cerca l'utente giusto
                        var loggedUser = db.Users.Where(model => model.Email == email && model.Password == password).FirstOrDefault();
                        if(loggedUser != null)
                        {
                            FormsAuthentication.SetAuthCookie(loggedUser.IdUser.ToString(), true);//salvo l'id ottenuto dalla select e lo passo al rolemanager
                            TempData["Success"] = "Logged in successfully";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    catch(Exception ex)
                    {
                        return View(ex.Message);
                    }
                }
                TempData["Fail"] = "Invalid credentials";
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            TempData["Fail"] = "Logged Out successfully";
            return RedirectToAction("Index", "Login");
        }
    }
}