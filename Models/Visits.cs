namespace ClinicaCaniZzoo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Visits
    {
        [Key]
        public int IdVisita { get; set; }

        public int? IdAnimale { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Data { get; set; }

        public string EsameObiettivo { get; set; }

        public string DescrizioneCura { get; set; }

        public decimal? Prezzo { get; set; }

        public virtual Animals Animals { get; set; }
    }
}
