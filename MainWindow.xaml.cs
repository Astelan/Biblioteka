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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZavrsniBiblioteka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonClan_Click(object sender, RoutedEventArgs e)
        {
            WindowClan w1 = new WindowClan();
            w1.Owner = this;
            w1.ShowDialog();
        }

        private void ButtonKnjige_Click(object sender, RoutedEventArgs e)
        {
            WindowKnjiga w2 = new WindowKnjiga();
            w2.Owner = this;
            w2.ShowDialog();
        }

        private void ButtonIznajmljivanje_Click(object sender, RoutedEventArgs e)
        {
            WindowIznajmljivanje w3 = new WindowIznajmljivanje();
            w3.Owner = this;
            w3.ShowDialog();
        }

        private void ButtonAdministracija_Click(object sender, RoutedEventArgs e)
        {
            WindowAdministracija w4 = new WindowAdministracija();
            w4.Owner = this;
            w4.ShowDialog();
        }
    }
}
