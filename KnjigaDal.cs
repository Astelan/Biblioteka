using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZavrsniBiblioteka
{
    class KnjigaDal
    {
        private BibliotekaZavrsni db = new BibliotekaZavrsni();

       
        public Knjiga VratiKnjige(int id)
        {
            return db.Knjige.Find(id);
        }

        public int UbaciKnjigu(Knjiga k)
        {
            try
            {
                db.Knjige.Add(k);
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                db.Entry(k).State = EntityState.Detached;
                return -1;
            }
        }

        public int PromeniKnjigu(Knjiga k)
        {
            try
            {
                db.Entry(k).State = EntityState.Modified;
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                db.Entry(k).State = EntityState.Unchanged;
                return -1;
            }

        }

        public int ObrisiKnjigu(Knjiga k)
        {
            try
            {
                db.Entry(k).State = EntityState.Deleted;
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                db.Entry(k).State = EntityState.Unchanged;
                return -1;
            }
        }

        public List<Zanr> ListaPretrage()
        {
            IQueryable<Zanr> zanrovi = db.Zanrovi.OrderBy(z => z.NazivZanra);
            List<Zanr> listaZanrova = zanrovi.ToList();
            
            return listaZanrova;
        }    

        public List<ViewKnjiga> ListaPoNaslovu()
        {
            IQueryable<ViewKnjiga> knjige = db.ViewKnjige.OrderBy(kg => kg.NazivKnjige);
            List<ViewKnjiga> lisatKnjiga = knjige.ToList();
            return lisatKnjiga;
        }

        public List<ViewKnjiga> ListaPoGodini()
        {
            IQueryable<ViewKnjiga> knjige = db.ViewKnjige.OrderBy(kg => kg.Godina);
            List<ViewKnjiga> lisatKnjiga = knjige.ToList();
            return lisatKnjiga;
        }

        public List<ViewKnjiga> VratiKnjige1()
        {
            string cnnBiblioteka = db.Database.Connection.ConnectionString;
            List<ViewKnjiga> listaIznajmljivanja = new List<ViewKnjiga>();

            using (SqlConnection konekcija = new SqlConnection(cnnBiblioteka))
            {
                SqlCommand komanda = new SqlCommand("SELECT * FROM ViewKnjiga", konekcija);
                try
                {
                    konekcija.Open();
                    using (SqlDataReader dr = komanda.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ViewKnjiga v1 = new ViewKnjiga
                            {
                                KnjigaId = dr.GetInt32(0),
                                NazivKnjige = dr.GetString(1),
                                ImePrezmeAutora = dr.GetString(2),
                                Godina = dr.GetInt32(3),
                                ISBN = dr.GetString(4),
                                Naziv = dr.GetString(5),
                                NazivZanra = dr.GetString(6)

                            };
                            listaIznajmljivanja.Add(v1);
                        }
                    }
                    return listaIznajmljivanja;
                }
                catch (Exception xcp)
                {
                    MessageBox.Show(xcp.Message);
                    return null;
                }


            }
        }

        public List<ViewKnjiga> Pretraga(string kolonaPretrage, string deoNaziva)
        {
            string cnnBiblioteka = db.Database.Connection.ConnectionString;
            List<ViewKnjiga> listaIznajmljivanja = new List<ViewKnjiga>();
            string izvrsna1 = $"SELECT * FROM ViewKnjiga WHERE {kolonaPretrage} LIKE '%{deoNaziva}%'";

            using (SqlConnection konekcija = new SqlConnection(cnnBiblioteka))
            {
                SqlCommand komanda = new SqlCommand(izvrsna1, konekcija);
                try
                {
                    konekcija.Open();
                    using (SqlDataReader dr = komanda.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ViewKnjiga v1 = new ViewKnjiga
                            {
                                KnjigaId = dr.GetInt32(0),
                                NazivKnjige = dr.GetString(1),
                                ImePrezmeAutora = dr.GetString(2),
                                Godina = dr.GetInt32(3),
                                ISBN = dr.GetString(4),
                                Naziv = dr.GetString(5),
                                NazivZanra = dr.GetString(6)

                            };
                            listaIznajmljivanja.Add(v1);
                        }
                    }
                    return listaIznajmljivanja;
                }
                catch (Exception xcp)
                {
                    MessageBox.Show(xcp.Message);
                    return null;
                }


            }
        }
    }
}
