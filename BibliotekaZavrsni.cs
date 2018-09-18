namespace ZavrsniBiblioteka
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BibliotekaZavrsni : DbContext
    {
        public BibliotekaZavrsni()
            : base("name=BibliotekaZavrsni")
        {
        }

        public virtual DbSet<Autor> Autori { get; set; }
        public virtual DbSet<Clan> Clanovi { get; set; }
        public virtual DbSet<Izdavac> Izdavaci { get; set; }
        public virtual DbSet<Iznajmljivanje> Iznajmljivanja { get; set; }
        public virtual DbSet<Knjiga> Knjige { get; set; }
        public virtual DbSet<Zanr> Zanrovi { get; set; }
        public virtual DbSet<ViewIznajmljivanja> ViewIznajmljivanja { get; set; }
        public virtual DbSet<ViewKnjiga> ViewKnjige { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>()
                .HasMany(e => e.Knjige)
                .WithRequired(e => e.Autor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clan>()
                .Property(e => e.Jmbg)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Clan>()
                .HasMany(e => e.Iznajmljivanja)
                .WithRequired(e => e.Clan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Izdavac>()
                .HasMany(e => e.Knjige)
                .WithRequired(e => e.Izdavac)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Knjiga>()
                .HasMany(e => e.Iznajmljivanja)
                .WithRequired(e => e.Knjiga)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Zanr>()
                .HasMany(e => e.Knjige)
                .WithRequired(e => e.Zanr)
                .WillCascadeOnDelete(false);
        }
    }
}
