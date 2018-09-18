namespace ZavrsniBiblioteka
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViewIznajmljivanja")]
    public partial class ViewIznajmljivanja
    {
        [Key]
        
        public int IznajmljivanjeId { get; set; }

        
        [StringLength(50)]
        public string NazivKnjige { get; set; }

        
        [StringLength(50)]
        public string Ime { get; set; }

        
        [StringLength(50)]
        public string Prezime { get; set; }

        
        [StringLength(10)]
        public string ClanskaKArta { get; set; }

        
        [Column(Order = 5, TypeName = "date")]
        public DateTime DatumIznajmljivanja { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DatumVracanja { get; set; }
    }
}
