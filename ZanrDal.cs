using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZavrsniBiblioteka
{
    class ZanrDal
    {
        private BibliotekaZavrsni db = new BibliotekaZavrsni();
        public List<Zanr> VratiZanrove()
        {
            return db.Zanrovi.ToList();
        }

        public int UbaciZanr(Zanr z)
        {
            try
            {
                db.Zanrovi.Add(z);
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {

                db.Entry(z).State = EntityState.Detached;
                return -1;
            }
        }

        public int PromeniZanr(Zanr z)
        {
            try
            {
                db.Entry(z).State = EntityState.Modified;
                db.SaveChanges();
                return 0;
            }
            catch (Exception cp)
            {
                MessageBox.Show(cp.ToString());
                db.Entry(z).State = EntityState.Unchanged;
                return -1;
            }
        }
    }
}
