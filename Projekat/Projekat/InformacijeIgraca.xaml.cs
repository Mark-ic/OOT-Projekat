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

namespace Projekat
{
    /// <summary>
    /// Interaction logic for InformacijeIgraca.xaml
    /// </summary>
    public partial class InformacijeIgraca : Window
    {
        Kosarkas kosarkas;
        public InformacijeIgraca(Kosarkas k)
        {
            InitializeComponent();
            this.kosarkas = k;
            Ime.Text = kosarkas.IME;
            Prezime.Text = kosarkas.PREZIME;
            Pozicija.Text = kosarkas.POZICIJA;
            Br_utakmica.Text = kosarkas.BR_UTAKMICA.ToString();
            Br_dresa.Text = kosarkas.BR_DRESA.ToString();
            Poeni.Text = kosarkas.BR_POENA.ToString();
            Nacionalnost.Text = kosarkas.NACIONALNOST;
            JMBG.Text = kosarkas.JMBG.ToString();
            string putanja = kosarkas.SLIKA;
            BitmapImage slifa = new BitmapImage();
            slifa.BeginInit();
            slifa.UriSource = new Uri(putanja, UriKind.RelativeOrAbsolute);
            slifa.EndInit();

            Slika.Source = slifa;
        }

        private void Dugme_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
