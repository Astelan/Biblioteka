using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZavrsniBiblioteka
{
    /// <summary>
    /// Interaction logic for WindowIznajmljivanje.xaml
    /// </summary>
    public partial class WindowIznajmljivanje : Window
    {
        private IznajmljivanjeDal izDal = new IznajmljivanjeDal();

        public WindowIznajmljivanje()
        {
            InitializeComponent();
        }

        private void PrikaziIznajmljivanja()
        {
            DataGrid1.ItemsSource = null;
            DataGrid1.ItemsSource = izDal.VratiIznajmljivanja();
            DataGrid1.SelectedValuePath = "IznajmljivanjeId";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PrikaziIznajmljivanja();
        }

        private void ButtonUbaci_Click(object sender, RoutedEventArgs e)
        {
            WindowIznajmljivanjePromena w1 = new WindowIznajmljivanjePromena();
            w1.Promena = 0;
            w1.Title = "Ubaci podatke o iznajmljivanju";
            w1.Owner = this;
            if (w1.ShowDialog() == true)
            {

                Clan c1 = w1.ComboBoxClan.SelectedItem as Clan;
                ViewKnjiga k1 = w1.ComboBoxKnjiga.SelectedItem as ViewKnjiga;

                Iznajmljivanje iz = new Iznajmljivanje
                {
                    ClanId = c1.ClanId,
                    KnjigaId = k1.KnjigaId,
                    DatumIznajmljivanja = w1.DatePicker1.SelectedDate.Value,
                    DatumVracanja = w1.DatePicker2.SelectedDate,

                };
                int rezultat = izDal.UbaciIznajmljivanje(iz);

                if (rezultat == 0)
                {

                    PrikaziIznajmljivanja();
                    DataGrid1.Focus();
                    DataGrid1.SelectedValue = iz.IznajmljivanjeId;
                    DataGrid1.ScrollIntoView(DataGrid1.SelectedItem);
                    MessageBox.Show("Uspesno ste izvrsili iznajmljivanje", "Poruka");

                }
                else
                {
                    MessageBox.Show("Greska pri unosu iznajmljivanja", "Poruka");
                }
            }

        }

        private void ButtonPromeni_Click(object sender, RoutedEventArgs e)
        {
            int indeks = DataGrid1.SelectedIndex;
            if (indeks < 0)
            {
                MessageBox.Show("Odaberite iznajmljivanje");
                return;
            }

            ViewIznajmljivanja v1 = DataGrid1.SelectedItem as ViewIznajmljivanja;
            int id = v1.IznajmljivanjeId;

            Iznajmljivanje iz1 = izDal.VratiIznajmjivanje(id);

            WindowIznajmljivanjePromena w1 = new WindowIznajmljivanjePromena();
            w1.Title = "Promeni podatke o iznajmljivanju";
            w1.Owner = this;
            w1.Promena = 1;
            w1.ComboBoxClan.SelectedValue = iz1.ClanId;
            w1.ComboBoxKnjiga.SelectedValue = iz1.KnjigaId;
            w1.DatePicker1.SelectedDate = iz1.DatumIznajmljivanja;
            w1.DatePicker2.SelectedDate = iz1.DatumVracanja;



            if (w1.ShowDialog() == true)
            {
                Clan c = w1.ComboBoxClan.SelectedItem as Clan;
                ViewKnjiga k2 = w1.ComboBoxKnjiga.SelectedItem as ViewKnjiga;

                iz1.ClanId = c.ClanId;
                iz1.KnjigaId = k2.KnjigaId;
                iz1.DatumIznajmljivanja = w1.DatePicker1.SelectedDate.Value;
                iz1.DatumVracanja = w1.DatePicker2.SelectedDate.Value;


                int rezultat = izDal.PromeniIznajmljivanje(iz1);
                if (rezultat == 0)
                {

                    PrikaziIznajmljivanja();
                    DataGrid1.Items.Refresh();
                    DataGrid1.Focus();

                    DataGrid1.SelectedIndex = indeks;
                    DataGrid1.ScrollIntoView(v1);
                    MessageBox.Show("Uspesno ste izmenili iznajmljivanje", "Iznajmljivanje promenjeno");
                }
                else
                {
                    MessageBox.Show("Greska pri izmeni iznajmljivanja", "Iznajmljivanje nije promenjeno");
                }
            }
        }

        private void ButtonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedIndex < 0)
            {
                return;
            }
            ViewIznajmljivanja red = (ViewIznajmljivanja)DataGrid1.SelectedItem;
            Iznajmljivanje iz = izDal.VratiIznajmjivanje(red.IznajmljivanjeId);

            if (MessageBox.Show("Da li ste siguran da zelite brisanje?", "Upozorenje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                int rezultat = izDal.ObrisiIznajmljivanje(iz);
                if (rezultat == 0)
                {
                    PrikaziIznajmljivanja();

                }
                else
                {
                    MessageBox.Show("Ne mozete obrisati Iznajmljivanje", "Poruka");
                }
            }
        }

        private void ButtonPretraga_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBPretraga.Text))
            {
                MessageBox.Show("Unesite deo broja članske karte");
            }
            else
            {
                DataGrid1.ItemsSource = izDal.Filtriraj(TBPretraga.Text.Trim());
            }
            
        }

        private void ButtonResetuj_Click(object sender, RoutedEventArgs e)
        {
            TBPretraga.Clear();
            PrikaziIznajmljivanja();
        }
    }
}
