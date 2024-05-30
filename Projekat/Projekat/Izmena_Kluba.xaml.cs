using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Izmena_Kluba.xaml
    /// </summary>
    public partial class Izmena_Kluba : Window
    {
        Klub klub;
        public Izmena_Kluba(Klub k)
        {
            InitializeComponent();
            klub = k;
            textIme.Text = klub.NAZIV;
            textMestp.Text = klub.MESTO;


               klubSlika.Source = new BitmapImage(new Uri(klub.LOGO, UriKind.RelativeOrAbsolute));

            
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            if(textIme.Text!="" && textMestp.Text != "")
            {

                klub.NAZIV = textIme.Text;
                klub.MESTO = textMestp.Text;
                MessageBox.Show("Uspesna izmena");
             
                this.Close();
            }
        }

        private void izmeni_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";


            if (openFileDialog.ShowDialog() == true)
            {

                string relativePath = "/logo_kluba/" + System.IO.Path.GetFileName(openFileDialog.FileName);
                klub.LOGO = relativePath;
                klubSlika.Source = new BitmapImage(new Uri(klub.LOGO, UriKind.RelativeOrAbsolute));

            }
        }
    }
}
