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
    /// Interaction logic for WindowClanPromena.xaml
    /// </summary>
    public partial class WindowClanPromena : Window
    {
        public WindowClanPromena()
        {
            InitializeComponent();
        }
        private bool Validacija()
        {
            if (string.IsNullOrWhiteSpace(TextBoxIme.Text))
            {
                MessageBox.Show("Unesite ime");
                TextBoxIme.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(TextBoxPrezime.Text))
            {
                MessageBox.Show("Unesite prezime");
                TextBoxPrezime.Focus();
                return false;
            }

            string mb = TextBoxJmbg.Text.Trim();

            if (mb.Length != 13)
            {
                MessageBox.Show("Maticni broj treba da ima tacno 13 karaktera");
                TextBoxJmbg.Focus();
                return false;
            }

            foreach (char c in mb)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(TextBoxAdresa.Text))
            {
                MessageBox.Show("Unesite adresu");
                TextBoxAdresa.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(TextBoxTelefon.Text))
            {
                MessageBox.Show("Unesite telefon");
                TextBoxTelefon.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(TextBoxClKarta.Text))
            {
                MessageBox.Show("Unesite broj clanske karte");
                TextBoxClKarta.Focus();
                return false;
            }
            return true;
        }

        private void ButtonPrihvati_Click(object sender, RoutedEventArgs e)
        {
            if (Validacija())
            {
                DialogResult = true;
            }
        }

        private void ButtonOdustani_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
