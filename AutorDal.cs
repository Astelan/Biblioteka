using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZavrsniBiblioteka
{
    class AutorDal
    {
        private BibliotekaZavrsni db = new BibliotekaZavrsni();
        public List<Autor> VratiAutore()
        {
            return db.Autori.ToList();
        }

        public int UbaciAutora(Autor a)
        {
            try
            {
                db.Autori.Add(a);
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {

                db.Entry(a).State = EntityState.Detached;
                return -1;
            }
        }

        public int PromeniAutora(Autor a)
        {
            try
            {
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
                return 0;
            }
            catch (Exception cp)
            {
                MessageBox.Show(cp.ToString());
                db.Entry(a).State = EntityState.Unchanged;
                return -1;
            }
        }
    }
}
