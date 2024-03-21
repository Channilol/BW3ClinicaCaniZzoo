namespace ClinicaCaniZzoo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Animals = new HashSet<Animals>();
            Sales = new HashSet<Sales>();
        }

        [Key]
        public int IdUser { get; set; }

        [StringLength(50)]
        [Required]
        public string Nome { get; set; }

        [StringLength(50)]
        [Required]
        public string Cognome { get; set; }

        [StringLength(16)]
        [Required]
        public string CodiceFiscale { get; set; }

        [StringLength(15)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Il campo telefono deve contenere solo numeri interi.")]
        public string Telefono { get; set; }

        [StringLength(100)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Ruolo { get; set; } = "User";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Animals> Animals { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sales> Sales { get; set; }
    }
}
