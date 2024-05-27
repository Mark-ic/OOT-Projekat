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

namespace Projekat
{
    /// <summary>
    /// Interaction logic for DodavanjeIgraca.xaml
    /// </summary>
    public partial class DodavanjeIgraca : Window
    {
        public DodavanjeIgraca()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double br_poena;
            int br_utakmica;
            int broj_dresa;
            long jmbg;
            Kosarkasi kosarkasi=new Kosarkasi();
            kosarkasi.import("igraci.txt");
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

                    if (kosarkasi.dodaj(new Kosarkas(jmbg, Ime.Text, Prezime.Text, Pozicija.Text, Nacionalnost.Text, broj_dresa, br_utakmica, br_poena, Slika.Text)))
                    {
                        MessageBox.Show("Uspesno ste uneli igraca");
                        kosarkasi.export("igraci.txt");
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
    }
}
