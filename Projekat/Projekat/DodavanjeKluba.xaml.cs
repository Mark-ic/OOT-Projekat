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
    /// Interaction logic for DodavanjeKluba.xaml
    /// </summary>
    public partial class DodavanjeKluba : Window
    {
        bool dodata = false;
        MainViewModel ma;
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
                string selectedFilePath = openFileDialog.FileName;

                string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string logoFolder = System.IO.Path.Combine(projectDirectory, "logo_kluba");

                string fileExtension = System.IO.Path.GetExtension(selectedFilePath);
                string fileName = textIme.Text + fileExtension;
                string savedFilePath = System.IO.Path.Combine(logoFolder, fileName);

                File.Copy(selectedFilePath, savedFilePath, true);

                string relativePath = "/logo_kluba/" + fileName;

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(savedFilePath, UriKind.Absolute);
                bitmap.EndInit();

                dodata = true;
                //MessageBox.Show("Image saved at: " + relativePath);
            }
        }
        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Klub klub;
            Random random = new Random();   
            if(textIme.Text !="" && textMestp.Text!="" )
            {
                if(dodata)
                {
                    string poznat = "bin/Debug/net6.0-windows/logo_kluba/"+textIme.Text+".png";
                    //MessageBox.Show("Putanja je: " + poznat);
                    klub=new Klub(random.Next(1500,int.MaxValue),textIme.Text,textMestp.Text,poznat);
                }
                else{
                    string nepoznat = "/slike_igraca/nepoznat.png";
                    klub = new Klub(random.Next(1500, int.MaxValue), textIme.Text, textMestp.Text, nepoznat);

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
