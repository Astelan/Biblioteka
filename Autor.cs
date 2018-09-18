namespace ZavrsniBiblioteka
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Autor")]
    public partial class Autor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Autor()
        {
            Knjige = new HashSet<Knjiga>();
        }

        public int AutorId { get; set; }

        [Required]
        [StringLength(50)]
        public string ImePrezmeAutora { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Knjiga> Knjige { get; set; }

        public override string ToString()
        {
            return ImePrezmeAutora;
        }
    }
}
