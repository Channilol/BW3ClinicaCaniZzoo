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

        [Required]
        public int? IdAnimale { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime? Data { get; set; }

        [Required]
        public string EsameObiettivo { get; set; }

        [Required]
        public string DescrizioneCura { get; set; }

        [Required]
        public decimal? Prezzo { get; set; }

        public virtual Animals Animals { get; set; }
    }
}
