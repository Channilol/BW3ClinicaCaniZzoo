namespace ClinicaCaniZzoo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sales
    {
        [Key]
        public int IdVendita { get; set; }

        public int? IdUser { get; set; }

        public int? IdProdotto { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataVendita { get; set; }

        public int? N_Ricetta { get; set; }

        public virtual Products Products { get; set; }

        public virtual Users Users { get; set; }
    }
}
