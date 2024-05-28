﻿using System;
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
    /// Interaction logic for IzmenaIgraca.xaml
    /// </summary>
    public partial class IzmenaIgraca : Window
    {
        Kosarkas kosarkas;
        public IzmenaIgraca(Kosarkas k)
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
            JMBG.Text= kosarkas.JMBG.ToString();
            Slika.Text = kosarkas.SLIKA;   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double br_poena;
            int br_utakmica;
            int broj_dresa;
            long jmbg;
            if (Ime.Text == "" || Prezime.Text == "" || Pozicija.Text == "" || Nacionalnost.Text == "" || Br_utakmica.Text == "" || Poeni.Text == "" || JMBG.Text == "" || Br_dresa.Text == "")
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
                    broj_dresa = int.Parse(Br_dresa.Text);
                    

                        kosarkas.IME=Ime.Text;
                        kosarkas.PREZIME=Prezime.Text;
                        kosarkas.BR_DRESA = broj_dresa;
                        kosarkas.NACIONALNOST=Nacionalnost.Text;
                        kosarkas.BR_UTAKMICA = br_utakmica;
                        kosarkas.BR_POENA = br_poena;
                        kosarkas.POZICIJA=Pozicija.Text;
                        kosarkas.JMBG = jmbg;
                        kosarkas.SLIKA = Slika.Text;

                    MessageBox.Show("Uspesno ste izmenili igraca.");

                }
                catch (Exception)
                {
                    MessageBox.Show("Doslo je do greske prilikom izmene podataka.");
                }


            }
        }
    }
}
