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
    /// Interaction logic for WindowClan.xaml
    /// </summary>
    public partial class WindowClan : Window
    {
        private ClanDal cDal = new ClanDal();

        public WindowClan()
        {
            InitializeComponent();
        }

        //Napuni podatke u DataGrid1
        private void NapunuClanove()
        {
            DataGrid1.ItemsSource = null;
            DataGrid1.ItemsSource = cDal.VratiClanove();
            DataGrid1.SelectedValuePath = "ClanId";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NapunuClanove();
        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indeks = DataGrid1.SelectedIndex;
            if (indeks > -1)
            {
                Clan cl1 = DataGrid1.SelectedItem as Clan;
                TextBoxIme.Text = cl1.Ime;
                TextBoxPrezime.Text = cl1.Prezime;
                TextBoxJmbg.Text = cl1.Jmbg;
                TextBoxAdresa.Text = cl1.Adresa;
                TextBoxTelefon.Text = cl1.Telefon;
                TextBoxClKarta.Text = cl1.ClanskaKArta;
            }
        }

        private void ButtonUbaci_Click(object sender, RoutedEventArgs e)
        {
            WindowClanPromena w1 = new WindowClanPromena();
            w1.Title = "Dodaj novog člana";
            w1.Owner = this;

            if (w1.ShowDialog() == true)
            {
                Clan c = new Clan
                {
                    Ime = w1.TextBoxIme.Text,
                    Prezime = w1.TextBoxPrezime.Text,
                    Jmbg = w1.TextBoxJmbg.Text,
                    Adresa = w1.TextBoxAdresa.Text,
                    Telefon = w1.TextBoxTelefon.Text,
                    ClanskaKArta = w1.TextBoxClKarta.Text
                };

                int rezultat = cDal.UbaciClana(c);

                if (rezultat == 0)
                {

                    NapunuClanove();
                    DataGrid1.SelectedValue = c.ClanId;
                    MessageBox.Show("Ubacen novi clan");
                }
                else
                {
                    MessageBox.Show("Greska pri unosu clana");
                }
            }
        }

        private void ButtonPromeni_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedIndex < 0)
            {
                return;
            }

            Clan c = DataGrid1.SelectedItem as Clan;
            WindowClanPromena w1 = new WindowClanPromena();
            w1.Owner = this;
            w1.Title = "Promeni podatke o clanu";
            w1.TextBoxIme.Text = c.Ime;
            w1.TextBoxPrezime.Text = c.Prezime;
            w1.TextBoxJmbg.Text = c.Jmbg;
            w1.TextBoxAdresa.Text = c.Adresa;
            w1.TextBoxTelefon.Text = c.Telefon;
            w1.TextBoxClKarta.Text = c.ClanskaKArta;

            if (w1.ShowDialog() == true)
            {
                c.Ime = w1.TextBoxIme.Text;
                c.Prezime = w1.TextBoxPrezime.Text;
                c.Jmbg = w1.TextBoxJmbg.Text;
                c.Adresa = w1.TextBoxAdresa.Text;
                c.Telefon = w1.TextBoxTelefon.Text;
                c.ClanskaKArta = w1.TextBoxClKarta.Text;

                int rezultat = cDal.PromeniClana(c);

                if (rezultat == 0)
                {
                    NapunuClanove();
                    DataGrid1.SelectedValue = c.ClanId;
                    MessageBox.Show("Podaci promenjeni");
                }
                else
                {
                    MessageBox.Show("Greska pri promeni");
                }
            }

        }

        private void ButtonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedIndex < 0)
            {
                return;
            }
            Clan c = DataGrid1.SelectedItem as Clan;

            MessageBoxResult mbr = MessageBox.Show("Da li zelite da obrisete clana " + c.ToString(), "Pitanje", MessageBoxButton.YesNo);

            if (mbr == MessageBoxResult.No)
            {
                return;
            }

            int rezultat = cDal.ObrisiClana(c);

            if (rezultat == 0)
            {
                NapunuClanove();
                TextBoxIme.Clear();
                TextBoxPrezime.Clear();
                TextBoxJmbg.Clear();
                TextBoxAdresa.Clear();
                TextBoxTelefon.Clear();
                TextBoxClKarta.Clear();
                DataGrid1.SelectedIndex = -1;
                MessageBox.Show("Obrisan clan");
            }

            else
            {
                MessageBox.Show("Greska pri brisanju clana");
            }
        }
    }
}
