using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.IO;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for DodavanjeIgraca.xaml
    /// </summary>
    public partial class DodavanjeIgraca : Window
    {
        MainViewModel model;
        public DodavanjeIgraca(MainViewModel mdl)
        {
            InitializeComponent();
            model = mdl;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double br_poena;
            int br_utakmica;
            int broj_dresa;
            long jmbg;
            if(Ime.Text=="" || Prezime.Text=="" || Pozicija.Text=="" || Nacionalnost.Text=="" || Br_utakmica.Text=="" || Poeni.Text=="" || JMBG.Text == "" || Br_dresa.Text=="")
            {
                MessageBox.Show("Niste uneli sve potrebne informacije vezane za igraca.");
            }
            else
            {
                try
                {
                    br_poena = Double.Parse(Poeni.Text);
                    jmbg = long.Parse(JMBG.Text);
                    br_utakmica = int.Parse(Br_utakmica.Text);
                    broj_dresa=int.Parse(Br_dresa.Text);
                    Kosarkas kosarkas = new Kosarkas();
                    if (model.dodajKosarkasa(new Kosarkas(jmbg, Ime.Text, Prezime.Text, Pozicija.Text, Nacionalnost.Text, broj_dresa, br_utakmica, br_poena, Slika.Text)))
                    {
                        MessageBox.Show("Uspesno ste uneli igraca");
                        kosarkas.Kosarkasi = model.Kosarkasi;
                        kosarkas.export("igraci.txt");
                        Ime.Text = "";
                        Prezime.Text = "";
                        Pozicija.Text = "";
                        Nacionalnost.Text = "";
                        Br_utakmica.Text = "";
                        Poeni.Text = "";  JMBG.Text = "";
                        Br_dresa.Text = "";
                        Slika.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Igrac sa tim brojem JMBG-a vec postoji.");
                    }


                }
                catch(Exception)
                {
                    MessageBox.Show("Doslo je do greske prilikom unosa podataka.");
                }
                

            }

        }

        private void Izaberi_sliku_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;

                string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string igraciFolder = System.IO.Path.Combine(projectDirectory, "slike_igraca");

                string fileExtension = System.IO.Path.GetExtension(selectedFilePath);
                string fileName = Ime.Text + fileExtension;
                string savedFilePath = System.IO.Path.Combine(igraciFolder, fileName);

                File.Copy(selectedFilePath, savedFilePath, true);

                string relativePath = "/slike_igraca/" + fileName;

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(savedFilePath, UriKind.Absolute);
                bitmap.EndInit();

                Slika.Text = relativePath;
            }
        }
    }
}
