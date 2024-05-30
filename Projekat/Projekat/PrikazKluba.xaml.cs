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
    /// Interaction logic for PrikazKluba.xaml
    /// </summary>
    public partial class PrikazKluba : Window
    {
        Klub klub;
        public PrikazKluba(Klub k)
        {
            InitializeComponent();
            klub = k;
            klub = k;
            textIme.Text = klub.NAZIV;
            textMestp.Text = klub.MESTO;
            klubSlika.Source = new BitmapImage(new Uri(klub.LOGO, UriKind.RelativeOrAbsolute));
        }

        private void zatvori_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
