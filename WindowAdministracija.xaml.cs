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
    /// Interaction logic for WindowAdministracija.xaml
    /// </summary>
    public partial class WindowAdministracija : Window
    {
        private AutorDal aDal = new AutorDal();
        private IzdavacDal izDal = new IzdavacDal();
        private ZanrDal zDal = new ZanrDal();

        public WindowAdministracija()
        {
            InitializeComponent();
        }

        private void NapunuAutore()
        {
            DataGridAutori.ItemsSource = null;
            DataGridAutori.ItemsSource = aDal.VratiAutore();
            DataGridAutori.SelectedValuePath = "AutorId";
        }

        private void NapunuIzdavace()
        {
            DataGridIzdavaci.ItemsSource = null;
            DataGridIzdavaci.ItemsSource = izDal.VratiIzdavace();
            DataGridIzdavaci.SelectedValuePath = "IzdavacId";
        }

        private void NapunuZanrove()
        {
            DataGridZanrovi.ItemsSource = null;
            DataGridZanrovi.ItemsSource = zDal.VratiZanrove();
            DataGridZanrovi.SelectedValuePath = "ZanrId";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NapunuAutore();
            NapunuIzdavace();
            NapunuZanrove();
        }

        private void DataGridAutori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indeks = DataGridAutori.SelectedIndex;
            if (indeks > -1)
            {
                Autor cl1 = DataGridAutori.SelectedItem as Autor;
                TextBoxAutor.Text = cl1.ImePrezmeAutora;
            }
        }

        private void DataGridIzdavaci_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indeks = DataGridIzdavaci.SelectedIndex;
            if (indeks > -1)
            {
                Izdavac izd = DataGridIzdavaci.SelectedItem as Izdavac;
                TextBoxNaziv.Text = izd.Naziv;
                TextBoxAdresa.Text = izd.Adresa;
                TextBoxTelefon.Text = izd.Telefon;
            }
        }

        private void DataGridZanrovi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indeks = DataGridZanrovi.SelectedIndex;
            if (indeks > -1)
            {
                Zanr zan = DataGridZanrovi.SelectedItem as Zanr;
                TextBoxZanr.Text = zan.NazivZanra;
            }
        }

        private void ButtonDodajtiAutor_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxAutor.Text))
            {
                MessageBox.Show("Unesite ime i prezime autora");
                return;
            }
            else
            {
                Autor au = new Autor
                {
                    ImePrezmeAutora = TextBoxAutor.Text
                };

                int rezultat = aDal.UbaciAutora(au);

                if (rezultat == 0)
                {

                    NapunuAutore();
                    DataGridAutori.SelectedValue = au.AutorId;
                    MessageBox.Show("Ubacen novi Autor");
                    TextBoxAutor.Clear();
                }
                else
                {
                    MessageBox.Show("Greska pri unosu clana");
                }
            }
        }

        private void ButtonPromeniAutor_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridAutori.SelectedIndex < 0)
            {
                return;
            }

            Autor au = DataGridAutori.SelectedItem as Autor;
            
            au.ImePrezmeAutora = TextBoxAutor.Text;

                    int rezultat = aDal.PromeniAutora(au);

                    if (rezultat == 0)
                    {
                        NapunuAutore();
                        DataGridAutori.SelectedValue = au.AutorId;
                        MessageBox.Show("Podaci promenjeni");
                    }
                    else
                    {
                        MessageBox.Show("Greska pri promeni");
                    }            

               

            
        }

        private void ButtonDodajtiZanr_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxZanr.Text))
            {
                MessageBox.Show("Unesite naziv žanra");
                return;
            }
            else
            {
                Zanr zr = new Zanr
                {
                    NazivZanra = TextBoxZanr.Text
                };

                int rezultat = zDal.UbaciZanr(zr);

                if (rezultat == 0)
                {

                    NapunuZanrove();
                    DataGridZanrovi.SelectedValue = zr.ZanrId;
                    MessageBox.Show("Ubacen novi Žanr");
                    TextBoxZanr.Clear();
                }
                else
                {
                    MessageBox.Show("Greska pri unosu clana");
                }
            }
        }

        private void ButtonPromeniZanr_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridZanrovi.SelectedIndex < 0)
            {
                return;
            }

            Zanr zan = DataGridZanrovi.SelectedItem as Zanr;

            zan.NazivZanra = TextBoxZanr.Text;

            int rezultat = zDal.PromeniZanr(zan);

            if (rezultat == 0)
            {
                NapunuZanrove();
                DataGridZanrovi.SelectedValue = zan.ZanrId;
                MessageBox.Show("Podaci promenjeni");
            }
            else
            {
                MessageBox.Show("Greska pri promeni");
            }
        }

        private void ButtonDodajtiIzdavac_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxNaziv.Text))
            {
                MessageBox.Show("Unesite naziv izdavača");
                return;
            }
            if (string.IsNullOrWhiteSpace(TextBoxAdresa.Text))
            {
                MessageBox.Show("Unesite adresu izdavača");
                return;
            }
            if (string.IsNullOrWhiteSpace(TextBoxTelefon.Text))
            {
                MessageBox.Show("Unesite telefon izdavača");
                return;
            }
            else
            {
                Izdavac izv = new Izdavac {
                    Naziv = TextBoxNaziv.Text,
                    Adresa = TextBoxAdresa.Text,
                    Telefon = TextBoxTelefon.Text
                };

                int rezultat = izDal.UbaciIzdavaca(izv);

                if (rezultat == 0)
                {

                    NapunuIzdavace();
                    DataGridIzdavaci.SelectedValue = izv.IzdavacId;
                    MessageBox.Show("Ubacen novi izdavač");
                }
                else
                {
                    MessageBox.Show("Greska pri unosu izdavača");
                }
            }
        }

        private void ButtonPromeniIzdavac_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridIzdavaci.SelectedIndex < 0)
            {
                return;
            }
            Izdavac izv = DataGridIzdavaci.SelectedItem as Izdavac;

            izv.Naziv = TextBoxNaziv.Text;
            izv.Adresa = TextBoxAdresa.Text;
            izv.Telefon = TextBoxTelefon.Text;

            int rezultat = izDal.PromeniIzdavaca(izv);

            if (rezultat == 0)
            {
                NapunuIzdavace();
                DataGridIzdavaci.SelectedValue = izv.IzdavacId;
                MessageBox.Show("Podaci promenjeni");
            }
            else
            {
                MessageBox.Show("Greska pri promeni");
            }
        }
    }
}
