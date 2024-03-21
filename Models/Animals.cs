namespace ClinicaCaniZzoo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Animals
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Animals()
        {
            Hospitalizations = new HashSet<Hospitalizations>();
            Visits = new HashSet<Visits>();
        }

        [Key]
        public int IdAnimale { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime? DataRegistrazione { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipologia { get; set; }

        [Required]
        [StringLength(50)]
        public string ColoreMantello { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataNascita { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^\S*$", ErrorMessage = "Il campo Microchip non può contenere spazi.")]
        public string Microchip { get; set; }

        public int? IdUser { get; set; } = 0;

        public virtual Users Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hospitalizations> Hospitalizations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visits> Visits { get; set; }
    }


    public class AnimalViewModel
    {
        public string Nome { get; set; }
        public string Tipologia { get; set; }
        public string ColoreMantello { get; set; }
        public string DataRegistrazione { get; set; }
        public string DataNascita { get; set; }
        public string NomePadrone { get; set; }
        public string CognomePadrone { get; set; }
    }

}
