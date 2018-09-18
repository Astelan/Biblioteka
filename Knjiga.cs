namespace ZavrsniBiblioteka
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Knjiga")]
    public partial class Knjiga
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Knjiga()
        {
            Iznajmljivanja = new HashSet<Iznajmljivanje>();
        }

        public int KnjigaId { get; set; }

        [Required]
        [StringLength(50)]
        public string NazivKnjige { get; set; }

        public int AutorId { get; set; }

        public int Godina { get; set; }

        [Required]
        [StringLength(50)]
        public string ISBN { get; set; }

        public int ZanrId { get; set; }

        public int IzdavacId { get; set; }

        public virtual Autor Autor { get; set; }

        public virtual Izdavac Izdavac { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Iznajmljivanje> Iznajmljivanja { get; set; }

        public virtual Zanr Zanr { get; set; }

        public override string ToString()
        {
            return NazivKnjige;
        }
    }
}
