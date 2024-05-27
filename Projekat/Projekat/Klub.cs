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
    public class Klub : INotifyPropertyChanged
    {
        private int id;
        private string naziv;
        private string mesto;
        private string logo;
        private double x;
        private double y;


        public event PropertyChangedEventHandler PropertyChanged;

        private Klub _odabraniKlub;
        public ObservableCollection<Klub> Klubovi { get; set; }
        public ObservableCollection<Klub> KluboviNaMapi { get; set; }

        public Klub OdabraniKlub
        {
            get { return _odabraniKlub; }
            set
            {
                if (_odabraniKlub != value)
                {
                    _odabraniKlub = value;
                    NotifyPropertyChanged(nameof(OdabraniKlub));
                }
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Klub()
        {
            this.id = 0;
            this.x = 0;
            this.y = 0;
            this.naziv = string.Empty;
            this.mesto = string.Empty;
            this.logo = string.Empty;
            OdabraniKlub = null;
            Klubovi = new ObservableCollection<Klub>();
        }

        public Klub(int id, string naziv, string mesto, string logo, double x = 0, double y = 0)
        {
            this.id = id;
            this.naziv = naziv;
            this.mesto = mesto;
            this.logo = logo;
            this.x = x;
            this.y = y;
            Klubovi = new ObservableCollection<Klub>();
        }

        public int ID
        {
            get { return id; }
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.NotifyPropertyChanged(nameof(ID));
                }
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

        public string NAZIV
        {
            get { return naziv; }
            set
            {
                if (this.naziv != value)
                {
                    this.naziv = value;
                    this.NotifyPropertyChanged(nameof(NAZIV));
                }
            }
        }

        public string MESTO
        {
            get { return mesto; }
            set
            {
                if (this.mesto != value)
                {
                    this.mesto = value;
                    this.NotifyPropertyChanged(nameof(MESTO));
                }
            }
        }

        public string LOGO
        {
            get { return logo; }
            set
            {
                if (this.logo != value)
                {
                    this.logo = value;
                    this.NotifyPropertyChanged(nameof(LOGO));
                }
            }
        }

        public void Import(string file)
        {
            int id;
            StreamReader sr = null;
            string linija;

            try
            {
                sr = new StreamReader(file);

                while ((linija = sr.ReadLine()) != null)
                {
                    string[] delovi = linija.Split('|');
                    id = int.Parse(delovi[0]);
                    if (Provera(id))
                    {
                        Klubovi.Add(new Klub(id, delovi[1], delovi[2], delovi[3]));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }

        public bool Provera(int id)
        {
            foreach (Klub klub in Klubovi)
            {
                if (klub.ID == id)
                {
                    return false;
                }
            }
            return true;
        }

        public ObservableCollection<Klub> KLUBOVI
        {
            get { return Klubovi; }
            set
            {
                if (this.Klubovi != value)
                {
                    this.Klubovi = value;
                    this.NotifyPropertyChanged(nameof(KLUBOVI));
                }
            }
        }
        
    }
}

