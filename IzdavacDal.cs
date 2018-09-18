using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZavrsniBiblioteka
{
    class IzdavacDal
    {
        private BibliotekaZavrsni db = new BibliotekaZavrsni();
        public List<Izdavac> VratiIzdavace()
        {
            return db.Izdavaci.ToList();
        }

        public int UbaciIzdavaca(Izdavac iz)
        {
            try
            {
                db.Izdavaci.Add(iz);
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {

                db.Entry(iz).State = EntityState.Detached;
                return -1;
            }
        }

        public int PromeniIzdavaca(Izdavac iz)
        {
            try
            {
                db.Entry(iz).State = EntityState.Modified;
                db.SaveChanges();
                return 0;
            }
            catch (Exception cp)
            {
                MessageBox.Show(cp.ToString());
                db.Entry(iz).State = EntityState.Unchanged;
                return -1;
            }
        }
    }
}
