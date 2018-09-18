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
    /// Interaction logic for WindowKnjigaPromena.xaml
    /// </summary>
    public partial class WindowKnjigaPromena : Window
    {
        private ZanrDal zDal = new ZanrDal();
        private AutorDal aDal = new AutorDal();
        private IzdavacDal iDal = new IzdavacDal();

        public WindowKnjigaPromena()
        {
            InitializeComponent();
        }

        private void PopuniComboBox()
        {
            ComboBoxZanr.ItemsSource = null;
            ComboBoxZanr.ItemsSource = zDal.VratiZanrove();
            ComboBoxZanr.SelectedValuePath = "ZanrId";

            ComboBoxAutor.ItemsSource = null;
            ComboBoxAutor.ItemsSource = aDal.VratiAutore();
            ComboBoxAutor.SelectedValuePath = "AutorId";

            ComboBoxIzdavac.ItemsSource = null;
            ComboBoxIzdavac.ItemsSource = iDal.VratiIzdavace();
            ComboBoxIzdavac.SelectedValuePath = "IzdavacId";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopuniComboBox();
        }

        private bool Validacija()
        {
            if (string.IsNullOrWhiteSpace(TextBoxNaziv.Text))
            {
                MessageBox.Show("Morate uneti naziv Knjige", "Poruka");
                TextBoxNaziv.Focus();
                return false;
            }


            if (!int.TryParse(TextBoxGodina.Text, out int godina))
            {
                MessageBox.Show("Godina izdnja knjige je ceo broj", "Poruka");
                TextBoxGodina.Clear();
                TextBoxGodina.Focus();
                return false;
            }

            if (ComboBoxZanr.SelectedIndex < 0)
            {
                MessageBox.Show("Morate uneti zanr knjige", "Poruka");

                return false;
            }

            if (string.IsNullOrWhiteSpace(TexboxISBN.Text))
            {
                MessageBox.Show("Morate uneti ISBN Knjige", "Poruka");
                TexboxISBN.Focus();
                return false;
            }

            if (ComboBoxAutor.SelectedIndex < 0)
            {
                MessageBox.Show("Morate izabrati autora", "Poruka");
                
                return false;
            }

            if (ComboBoxIzdavac.SelectedIndex < 0)
            {
                MessageBox.Show("Morate izabrati izdavača", "Poruka");

                return false;
            }

            return true;
        }

        private void ButtonOdustani_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ButtonPrihvati_Click(object sender, RoutedEventArgs e)
        {
            if (Validacija())
            {
                DialogResult = true;
            }
        }
    }
}
