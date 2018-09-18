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
    /// Interaction logic for WindowIznajmljivanjePromena.xaml
    /// </summary>
    public partial class WindowIznajmljivanjePromena : Window
    {
        public int Promena { get; set; }
        private ClanDal cdal = new ClanDal();
        KnjigaDal kdal = new KnjigaDal();

        public WindowIznajmljivanjePromena()
        {
            InitializeComponent();
        }

        private void PrikaziClanove()
        {
            ComboBoxClan.ItemsSource = null;
            ComboBoxClan.ItemsSource = cdal.VratiClanove();
            ComboBoxClan.SelectedValuePath = "ClanId";
        }

        private void PrikaziKnjige()
        {
            ComboBoxKnjiga.ItemsSource = null;
            ComboBoxKnjiga.ItemsSource = kdal.VratiKnjige1();
            ComboBoxKnjiga.SelectedValuePath = "KnjigaId";
        }

        private bool Validacija()
        {
            if (ComboBoxClan.SelectedIndex < 0)
            {
                MessageBox.Show("Morate odabrati clana", "Poruka");
                return false;
            }

            if (ComboBoxKnjiga.SelectedIndex < 0)
            {
                MessageBox.Show("Morate odabrati knjigu", "Poruka");
                return false;
            }

            if (DatePicker1.SelectedDate == null)
            {
                MessageBox.Show("Morate odabrati datum uzimanja", "Poruka");
                return false;
            }
            if (DatePicker2.SelectedDate == null)
            {
                MessageBox.Show("Morate odabrati datum vracanja", "Poruka");
                return false;
            }

            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Promena == 0)
            {
                DatePicker1.SelectedDate = DateTime.Now;
                DatePicker2.SelectedDate = DateTime.Now.AddDays(7);
            }

            PrikaziClanove();
            PrikaziKnjige();
        }

        private void ButtonPotvrdi_Click(object sender, RoutedEventArgs e)
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
