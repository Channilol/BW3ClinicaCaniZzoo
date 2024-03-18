namespace ClinicaCaniZzoo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Hospitalizations
    {
        [Key]
        public int IdRicovero { get; set; }

        [Required]
        public int? IdAnimale { get; set; }

        [Required]
        [StringLength(50)]
        public string StatoRicovero { get; set; }

        [Required]
        public string FotoAnimale { get; set; }

        public virtual Animals Animals { get; set; }
    }
}
