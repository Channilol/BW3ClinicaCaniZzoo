using ClinicaCaniZzoo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    [Authorize (Roles = "AdminF")]
    public class MedicinaliController : Controller
    {

        public ActionResult ElencoMedicinali()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetElencoMedicinali([Bind(Include = "codiceFiscale")]string codiceFiscale)
        {
            var connString = ConfigurationManager.ConnectionStrings["DBContext"].ToString();
            List<Medicinale> elencoMedicinali = new List<Medicinale>();
            try
            {
                string query = "SELECT u.Cognome, u.Nome, u.CodiceFiscale, p.NomeProdotto, s.DataVendita, s.N_Ricetta FROM Sales AS s JOIN Users AS u ON u.IdUser = s.IdUser JOIN Products AS p ON p.IdProdotto = s.IdProdotto WHERE u.CodiceFiscale = @codiceFiscale";
                var conn = new SqlConnection(connString);
                conn.Open();
                var command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@codiceFiscale", codiceFiscale);
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Medicinale medicinale = new Medicinale
                        {
                            Cognome = reader["Cognome"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            CodiceFiscale = reader["CodiceFiscale"].ToString(),
                            NomeProdotto = reader["NomeProdotto"].ToString(),
                            DataVendita = (DateTime)reader["DataVendita"],
                            N_Ricetta = (reader["N_Ricetta"] != DBNull.Value) ? (int)reader["N_Ricetta"] : 0,

                    };
                        elencoMedicinali.Add(medicinale);
                    }
                    ViewBag.ElencoMedicinali = elencoMedicinali;
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            return View("ElencoMedicinali");
        }
    }

}
