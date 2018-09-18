namespace ZavrsniBiblioteka
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Izdavac")]
    public partial class Izdavac
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Izdavac()
        {
            Knjige = new HashSet<Knjiga>();
        }

        public int IzdavacId { get; set; }

        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }

        [Required]
        [StringLength(50)]
        public string Adresa { get; set; }

        [Required]
        [StringLength(50)]
        public string Telefon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Knjiga> Knjige { get; set; }

        public override string ToString()
        {
            return Naziv;
        }
    }
}
