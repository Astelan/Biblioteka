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
    /// Interaction logic for WindowKnjiga.xaml
    /// </summary>
    public partial class WindowKnjiga : Window
    {
        private KnjigaDal kDal = new KnjigaDal();
        private ZanrDal zDal = new ZanrDal();
        

        private void PrikaziKnjige()
        {
            DataGrid1.ItemsSource = null;
            DataGrid1.ItemsSource = kDal.VratiKnjige1();
            DataGrid1.SelectedValuePath = "KnjigeId";
        }
         
        public WindowKnjiga()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            ComboBoxPretraga.ItemsSource = kDal.ListaPretrage();
            ComboBoxPretraga.DisplayMemberPath = "NazivZanra";

            PrikaziKnjige();

            ComboBoxKolina.Items.Add("NazivKnjige");
            ComboBoxKolina.Items.Add("ImePrezmeAutora");

        }

        private void DataGrid1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (DataGrid1.SelectedIndex < 0)
            {
                return;
            }
            ViewKnjiga k1 = DataGrid1.SelectedItem as ViewKnjiga;
            TextBoxNaziv.Text = k1.NazivKnjige;
            TextBoxGodina.Text = k1.Godina.ToString();
            TextBoxZanr.Text = k1.NazivZanra;
            TexboxAutor.Text = k1.ImePrezmeAutora;
            TexboxISBI.Text = k1.ISBN;
            TexboxIzdavac.Text = k1.Naziv;
        }

        private void ComboBoxPretraga_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxPretraga.SelectedIndex > -1)
            {
                BibliotekaZavrsni db = new BibliotekaZavrsni();
                Zanr z1 = (Zanr)ComboBoxPretraga.SelectedItem;
                                
                var listaKnjige = db.ViewKnjige.Where(kg => kg.NazivZanra == z1.NazivZanra).OrderBy(kg => kg.NazivZanra);
                DataGrid1.ItemsSource = listaKnjige.ToList();
                
            }
        }

        private void ButtonResetuj_Click(object sender, RoutedEventArgs e)
        {
            TextBoxPretraga.Clear();
            ComboBoxKolina.SelectedIndex = -1;
            PrikaziKnjige();
        }

        private void ButtonUbaci_Click(object sender, RoutedEventArgs e)
        {
            WindowKnjigaPromena w1 = new WindowKnjigaPromena();
            w1.Owner = this;
            w1.Title = "Unesi podatke o knjizi";
            if (w1.ShowDialog() == true)
            {
                Knjiga k1 = new Knjiga
                {
                    NazivKnjige = w1.TextBoxNaziv.Text.ToUpper(),
                    Godina = int.Parse(w1.TextBoxGodina.Text),
                    ZanrId = (int)w1.ComboBoxZanr.SelectedValue,
                    AutorId = (int)w1.ComboBoxAutor.SelectedValue,
                    ISBN = w1.TexboxISBN.Text,
                    IzdavacId = (int)w1.ComboBoxIzdavac.SelectedValue,
                };

                int rezultat = kDal.UbaciKnjigu(k1);

                if (rezultat == 0)
                {
                    MessageBox.Show("Uspesno ste ubacili knjigu", "Poruka");
                    PrikaziKnjige();
                    DataGrid1.SelectedValue = k1.KnjigaId;
                    DataGrid1.ScrollIntoView(k1);
                }
                else
                {
                    MessageBox.Show("Greska pri unosu knjige", "Poruka");
                }

            }
        }

        private void ButtonPromeni_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedIndex < 0)
            {
                MessageBox.Show("Odaberite Knjigu");
                return;
            }

            ViewKnjiga v1 = DataGrid1.SelectedItem as ViewKnjiga;
            int id = v1.KnjigaId;

            Knjiga kg1 = kDal.VratiKnjige(id);

            WindowKnjigaPromena w1 = new WindowKnjigaPromena();
            w1.Title = "Promeni podatke o Knjizi";
            w1.Owner = this;

            w1.TextBoxNaziv.Text = kg1.NazivKnjige;
            w1.TextBoxGodina.Text = kg1.Godina.ToString();
            w1.ComboBoxZanr.SelectedValue = kg1.ZanrId;
            w1.ComboBoxAutor.SelectedValue = kg1.AutorId;
            w1.TexboxISBN.Text = kg1.ISBN;
            w1.ComboBoxIzdavac.SelectedValue = kg1.IzdavacId;


            if (w1.ShowDialog() == true)
            {
                kg1.NazivKnjige = w1.TextBoxNaziv.Text;
                kg1.Godina = int.Parse(w1.TextBoxGodina.Text);
                kg1.ZanrId = (int)w1.ComboBoxZanr.SelectedValue;
                kg1.AutorId = (int) w1.ComboBoxAutor.SelectedValue;
                kg1.ISBN = w1.TexboxISBN.Text;
                kg1.IzdavacId = (int)w1.ComboBoxIzdavac.SelectedValue;


                int rezultat = kDal.PromeniKnjigu(kg1);
                if (rezultat == 0)
                {
                    PrikaziKnjige();
                    DataGrid1.SelectedValue = kg1.KnjigaId;
                    DataGrid1.ScrollIntoView(kg1);
                    MessageBox.Show("Uspesno ste izmenili knjigu", "Knjiga promenjen");
                }
                else
                {
                    MessageBox.Show("Greska pri izmeni Knjiga", "Knjiga nije promenjen");
                }
            }
        }

        private void ButtonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedIndex < 0)
            {
                return;
            }
            ViewKnjiga red = (ViewKnjiga)DataGrid1.SelectedItem;
            Knjiga kg2 = kDal.VratiKnjige(red.KnjigaId);
            

            if (MessageBox.Show("Da li ste siguran da zelite brisanje?", "Upozorenje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                int rez = kDal.ObrisiKnjigu(kg2);
                if (rez == 0)
                {
                    PrikaziKnjige();
                    DataGrid1.SelectedIndex = -1;
                    TextBoxNaziv.Clear();
                    TextBoxGodina.Clear();
                    TextBoxZanr.Clear();
                    TexboxAutor.Clear();
                    TexboxISBI.Clear();
                    TexboxIzdavac.Clear();

                }
                else
                {
                    MessageBox.Show("Ne mozete obrisati Knjigu");
                }
            }
        }


        //SORTIRANJE
        private void ButtonUzlazno_Click(object sender, RoutedEventArgs e)
        {
             
            var listaKnjige = kDal.ListaPoNaslovu();
            DataGrid1.ItemsSource = listaKnjige.ToList();
        }

        private void ButtonSilazno_Click(object sender, RoutedEventArgs e)
        {
            var listaKnjige = kDal.ListaPoGodini();
            DataGrid1.ItemsSource = listaKnjige.ToList();
        }

        private void ButtonPretraga_Click(object sender, RoutedEventArgs e)
        {
            
            if (ComboBoxKolina.SelectedIndex < 0)
            {
                MessageBox.Show("Morate Odaberite kolonnu za pretragu");

                return;
            }
            if (string.IsNullOrWhiteSpace(TextBoxPretraga.Text))
            {
                MessageBox.Show("Unesite deo reči za pretragu");
                return;
            }
            else
            {
                DataGrid1.ItemsSource = kDal.Pretraga(ComboBoxKolina.SelectedItem.ToString(), TextBoxPretraga.Text.Trim());
            }
        }
    }
}
