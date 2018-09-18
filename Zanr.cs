namespace ZavrsniBiblioteka
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Zanr")]
    public partial class Zanr
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Zanr()
        {
            Knjige = new HashSet<Knjiga>();
        }

        public int ZanrId { get; set; }

        [Required]
        [StringLength(50)]
        public string NazivZanra { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Knjiga> Knjige { get; set; }

        public override string ToString()
        {
            return NazivZanra;
        }
    }
}
