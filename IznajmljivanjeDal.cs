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
    class IznajmljivanjeDal
    {
        private BibliotekaZavrsni db = new BibliotekaZavrsni();

        public List<ViewIznajmljivanja> VratiIznajmljivanja()
        {
            string cnnBiblioteka = db.Database.Connection.ConnectionString;
            List<ViewIznajmljivanja> listaIznajmljivanja = new List<ViewIznajmljivanja>();

            using (SqlConnection konekcija = new SqlConnection(cnnBiblioteka))
            {
                SqlCommand komanda = new SqlCommand("SELECT * FROM ViewIznajmljivanja", konekcija);
                try
                {
                    konekcija.Open();
                    using (SqlDataReader dr = komanda.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ViewIznajmljivanja v1 = new ViewIznajmljivanja
                            {
                                IznajmljivanjeId = dr.GetInt32(0),
                                NazivKnjige = dr.GetString(1),
                                Ime = dr.GetString(2),
                                Prezime = dr.GetString(3),
                                ClanskaKArta = dr.GetString(4),
                                DatumIznajmljivanja = dr.GetDateTime(5),
                                DatumVracanja = dr.GetDateTime(6)                               

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

        public Iznajmljivanje VratiIznajmjivanje(int id)
        {
            return db.Iznajmljivanja.Find(id);
        }

        public int UbaciIznajmljivanje(Iznajmljivanje iz)
        {
            try
            {
                db.Iznajmljivanja.Add(iz);
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                db.Entry(iz).State = EntityState.Detached;
                return -1;
            }
        }

        public int PromeniIznajmljivanje(Iznajmljivanje iz)
        {
            try
            {
                db.Entry(iz).State = EntityState.Modified;
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                db.Entry(iz).State = EntityState.Unchanged;
                return -1;
            }

        }

        public int ObrisiIznajmljivanje(Iznajmljivanje iz)
        {
            try
            {
                db.Entry(iz).State = EntityState.Deleted;
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                db.Entry(iz).State = EntityState.Unchanged;
                return -1;
            }
        }

        public List<ViewIznajmljivanja> Filtriraj(string deoNaziva)
        {
            string cnnBiblioteka = db.Database.Connection.ConnectionString;
            List<ViewIznajmljivanja> listaIznajmljivanja = new List<ViewIznajmljivanja>();
            string izvrsna = $"SELECT * FROM ViewIznajmljivanja WHERE ClanskaKArta LIKE '{deoNaziva}%'";


            using (SqlConnection konekcija = new SqlConnection(cnnBiblioteka))
            {
                SqlCommand komanda = new SqlCommand(izvrsna, konekcija);
                try
                {
                    konekcija.Open();
                    using (SqlDataReader dr = komanda.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ViewIznajmljivanja v1 = new ViewIznajmljivanja
                            {
                                IznajmljivanjeId = dr.GetInt32(0),
                                NazivKnjige = dr.GetString(1),
                                Ime = dr.GetString(2),
                                Prezime = dr.GetString(3),
                                ClanskaKArta = dr.GetString(4),
                                DatumIznajmljivanja = dr.GetDateTime(5),
                                DatumVracanja = dr.GetDateTime(6)

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
