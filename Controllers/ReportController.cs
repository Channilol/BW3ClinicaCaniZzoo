using ClinicaCaniZzoo.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ClinicaCaniZzoo.Controllers
{
    public class ReportController : Controller
    {
        private readonly DBContext _context = new DBContext();

        public async Task<ActionResult> VenditeInData(string data)
        {
            DateTime? filtroData = null;
            if (!string.IsNullOrEmpty(data))
            {
                filtroData = DateTime.Parse(data);
            }

         
            var query = _context.Products
                    .Include(p => p.Sales.Select(s => s.Users)) 
                    .AsQueryable();


            if (filtroData.HasValue)
            {
                int year = filtroData.Value.Year;
                int month = filtroData.Value.Month;
                int day = filtroData.Value.Day;

                query = query.Where(p => p.Sales.Any(s => s.DataVendita.Value.Year == year &&
                                                           s.DataVendita.Value.Month == month &&
                                                           s.DataVendita.Value.Day == day));
            }

            var prodottiConVendite = await query.ToListAsync();

            ViewBag.Data = data;
            return View(prodottiConVendite);
        }


    }
}
