using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class Kosarkas : INotifyPropertyChanged
    {
        private int jmbg;
        private string ime;
        private string prezime;
        private string pozicija;
        private string nacionalnost;
        private int br_dresa;
        private int br_utakmica;
        private int br_poena;
        private string slika;



        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string v)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

        public Kosarkas()
        {
            jmbg = 0;
            ime= string.Empty;
            prezime = string.Empty;
            pozicija = string.Empty;
            nacionalnost = string.Empty;
            slika = string.Empty;
            br_dresa = 0;
            br_utakmica = 0;
            br_poena = 0;

        }

        public Kosarkas
            (
            int jmbg, string ime, string prezime,
            string pozicija, string nacionalnost,
            int br_dresa, int br_utakmica,
            int br_poena, string slika
            )
        {
            this.jmbg = jmbg;
            this.ime = ime;
            this.prezime = prezime;
            this.pozicija = pozicija;
            this.nacionalnost = nacionalnost;
            this.br_dresa = br_dresa;
            this.br_utakmica = br_utakmica;
            this.br_poena = br_poena;
            this.slika = slika;
        }

        public int JMBG 
        { 
            get { return jmbg; } 
            set 
            {
                if (this.jmbg != value)
                {
                    this.jmbg = value;
                    this.NotifyPropertyChanged("JMBG");
                }
            }
        
        }

        public int BR_DRESA
        {
            get { return br_dresa; }
            set
            {
                if (this.br_dresa != value)
                {
                    this.br_dresa = value;
                    this.NotifyPropertyChanged("BR_DRESA");
                }
            }

        }

        public int BR_UTAKMICA
        {
            get { return br_utakmica; }
            set
            {
                if (this.br_utakmica != value)
                {
                    this.br_utakmica = value;
                    this.NotifyPropertyChanged("BR_UTAKMICA");
                }
            }

        }

        public int BR_POENA
        {
            get { return br_poena; }
            set
            {
                if (this.br_poena != value)
                {
                    this.br_poena = value;
                    this.NotifyPropertyChanged("BR_POENA");
                }
            }

        }

        public string IME
        {
            get { return ime; }
            set
            {
                if (this.ime != value)
                {
                    this.ime = value;
                    this.NotifyPropertyChanged("IME");
                }
            }

        }

        public string PREZIME
        {
            get { return prezime; }
            set
            {
                if (this.prezime != value)
                {
                    this.prezime = value;
                    this.NotifyPropertyChanged("PREZIME");
                }
            }

        }

        public string POZICIJA
        {
            get { return pozicija; }
            set
            {
                if (this.pozicija != value)
                {
                    this.pozicija = value;
                    this.NotifyPropertyChanged("POZICIJA");
                }
            }

        }
        public string NACIONALNOST
        {
            get { return nacionalnost; }
            set
            {
                if (this.nacionalnost != value)
                {
                    this.nacionalnost = value;
                    this.NotifyPropertyChanged("NACIONALNOST");
                }
            }

        }

        public string SLIKA
        {
            get { return slika; }
            set
            {
                if (this.slika != value)
                {
                    this.slika = value;
                    this.NotifyPropertyChanged("SLIKA");
                }
            }

        }
    }
}
