using ClinicaCaniZzoo.Models;
using System;
using System.Collections.Generic;
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

            var vendite = new List<Sales>();

            if (filtroData.HasValue)
            {
                vendite = await _context.Sales
                            .Where(s => DbFunctions.TruncateTime(s.DataVendita) == DbFunctions.TruncateTime(filtroData.Value))
                            .Include(s => s.Products)
                            .Include(s => s.Users)
                            .ToListAsync();
            }
            else
            {
                
                vendite = await _context.Sales
                            .Include(s => s.Products)
                            .Include(s => s.Users)
                            .ToListAsync();
            }

            ViewBag.Data = data;
            return View(vendite);
        }

    }
}
