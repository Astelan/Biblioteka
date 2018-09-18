using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ZavrsniBiblioteka
{
    [Table("ViewKnjiga")]
    public partial class ViewKnjiga
    {
        [Key]
        public int KnjigaId { get; set; }

        [StringLength(50)]
        public string NazivKnjige { get; set; }

        [StringLength(50)]
        public string ImePrezmeAutora { get; set; }

        public int Godina { get; set; }

        
        [StringLength(50)]
        public string ISBN { get; set; }

        
        [StringLength(50)]
        public string Naziv { get; set; }

        [StringLength(50)]
        public string NazivZanra { get; set; }

        public override string ToString()
        {
            return NazivKnjige;
        }
    }       
}
