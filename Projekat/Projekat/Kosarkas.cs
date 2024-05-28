using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projekat
{
    public class Kosarkas : INotifyPropertyChanged
    {
        private long jmbg;
        private string ime;
        private string prezime;
        private string pozicija;
        private string nacionalnost;
        private int br_dresa;
        private int br_utakmica;
        private double br_poena;
        private string slika;
        private double x;
        private double y;

        private Kosarkas _odabraniKosarkas;
        public ObservableCollection<Kosarkas> Kosarkasi{ get; set; }
        public ObservableCollection<Kosarkas> KosarkasiNaTerenu { get; set; }

        public Kosarkas OdabraniKosarkas
        {
            get { return _odabraniKosarkas; }
            set
            {
                if (_odabraniKosarkas != value)
                {
                    _odabraniKosarkas = value;
                    NotifyPropertyChanged(nameof(OdabraniKosarkas));
                }
            }
        }


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
            x = 0;
            y = 0;
            Kosarkasi = new ObservableCollection<Kosarkas>();

        }

        public Kosarkas
            (
            long jmbg, string ime, string prezime,
            string pozicija, string nacionalnost,
            int br_dresa, int br_utakmica,
            double br_poena, string slika,double x=0,double y = 0
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
            OdabraniKosarkas = null;
            Kosarkasi = new ObservableCollection<Kosarkas>();
            this.x = x;
            this.y = y;
            if (slika != "")
            {
                this.slika = slika;
            }
            else
            {
                this.slika = "/slike_igraca/nepoznat.png";
            }
        }
        public double X
        {
            get { return x; }
            set
            {
                if (this.x != value)
                {
                    this.x = value;
                    this.NotifyPropertyChanged(nameof(X));
                }
            }
        }

        public double Y
        {
            get { return y; }
            set
            {
                if (this.y != value)
                {
                    this.y = value;
                    this.NotifyPropertyChanged(nameof(Y));
                }
            }
        }
        public ObservableCollection<Kosarkas> KOSARKASI
        {
            get { return Kosarkasi; }
            set
            {
                if (this.Kosarkasi != value)
                {
                    this.Kosarkasi = value;
                    this.NotifyPropertyChanged(nameof(KOSARKASI));
                }
            }
        }

        public long JMBG 
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

        public double BR_POENA
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
        public override string? ToString()
        {
            string str = JMBG + "|" + IME + "|" + PREZIME + "|" + POZICIJA + "|" + NACIONALNOST + "|" + BR_DRESA + "|" + BR_UTAKMICA + "|" + BR_POENA + "|" + SLIKA;
            return str;
        }

        public string zaTabelu()
        {
            string str = IME + "," + PREZIME + "," + POZICIJA + "," + BR_POENA;
            return str;
        }
        public bool dodaj(Kosarkas kosarkas)
        {
            foreach (Kosarkas item in Kosarkasi)
            {
                if (kosarkas.JMBG == item.JMBG)
                {
                    return false;
                }
            }
            Kosarkasi.Add(kosarkas);
            return true;
        }

        public bool provera(long jmbg)
        {
            foreach (Kosarkas kosarkas in Kosarkasi)
            {
                if (kosarkas.JMBG == jmbg)
                {
                    return false;
                }
            }
            return true;
        }

        public void import(string file)
        {
            StreamReader sr = null;
            string linija;
            long jmbg;
            try
            {
                sr = new StreamReader(file);

                while ((linija = sr.ReadLine()) != null)
                {
                    string[] delovi = linija.Split('|');
                    jmbg = long.Parse(delovi[0]);
                    if (provera(jmbg))
                    {
                        Kosarkasi.Add(new Kosarkas(jmbg, delovi[1], delovi[2], delovi[3], delovi[4], int.Parse(delovi[5]), int.Parse(delovi[6]), double.Parse(delovi[7]), delovi[8]));
                    }

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }

        public void export(string file)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(file);
                foreach (Kosarkas k in Kosarkasi)
                {
                    sw.WriteLine(k.ToString());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                if (sw != null) sw.Close();
            }
        }
    }
}
