using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for DodavanjeKluba.xaml
    /// </summary>
    public partial class DodavanjeKluba : Window
    {
        bool dodata = false;
        MainViewModel ma;
        string naziv_slike;
        public DodavanjeKluba(MainViewModel md)
        {
            InitializeComponent();
            ma = md;    
        }

        private void Dugme_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";


            if (openFileDialog.ShowDialog() == true)
            {
                
                naziv_slike= System.IO.Path.GetFileName(openFileDialog.FileName);
                dodata = true;
            
            }
        }
        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Klub klub;
            Random random = new Random();
            Regex regex = new Regex("^[a-zA-Z]+$");
            bool hasOnlyAlpha = regex.IsMatch(textMestp.Text);
            if (!hasOnlyAlpha)
            {
                MessageBox.Show("Polje mesto sadrzi brojeve!!!!");
            }
            else
            if (textIme.Text !="" && textMestp.Text!="" && hasOnlyAlpha )
            {
                int broj = random.Next(1500, int.MaxValue);
                foreach(Klub k in ma.Klubovi)
                {
                    if(k.ID == broj)
                    {
                        broj= random.Next(1500, int.MaxValue);
                    }
                }
                if (dodata)
                {
                    string poznat = "/logo_kluba/" + naziv_slike;
                    //MessageBox.Show("Putanja je: " + poznat);
                    klub =new Klub(broj,textIme.Text,textMestp.Text,poznat);
                }
                else{
                    string nepoznat = "/slike_igraca/nepoznat.png";
                    klub = new Klub(broj, textIme.Text, textMestp.Text, nepoznat);

                }
                if (!ma.Klubovi.Contains(klub))
                {
                    MessageBox.Show("Uspesno dodato!");
                    textIme.Text = "";
                    textMestp.Text = "";
                    ma.Klubovi.Add(klub);
                }
            }
            else
            {
                MessageBox.Show("Nisu sva polja popunjena");
            }
        }
    }
}
