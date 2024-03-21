using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaCaniZzoo.Models
{
    public class Medicinale
    {
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string CodiceFiscale { get; set; }
        public string NomeProdotto { get; set; }
        public DateTime DataVendita { get; set; }
        public int N_Ricetta { get; set; }
    }
}