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
    class ClanDal
    {
        private BibliotekaZavrsni db = new BibliotekaZavrsni();
        public List<Clan> VratiClanove()
        {
            return db.Clanovi.ToList();
        }

        //Detačovan objekat
        public int UbaciClana(Clan c)
        {
            try
            {
                db.Clanovi.Add(c);
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {

                db.Entry(c).State = EntityState.Detached;
                return -1;
            }
        }

        public int PromeniClana(Clan c)
        {
            try
            {
                db.Entry(c).State = EntityState.Modified;
                db.SaveChanges();
                return 0;
            }
            catch (Exception cp)
            {
                MessageBox.Show(cp.ToString());
                db.Entry(c).State = EntityState.Unchanged;
                return -1;
            }
        }

        public int ObrisiClana(Clan c)
        {
            try
            {
                db.Entry(c).State = EntityState.Deleted;
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Ne možete obrisati člana zbog zaduženja");
                db.Entry(c).State = EntityState.Unchanged;
                return -1;
            }
        }

        

       
    }
}
