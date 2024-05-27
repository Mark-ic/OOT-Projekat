using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projekat
{
    internal class Kosarkasi : INotifyPropertyChanged
    {
        private List<Kosarkas> lista;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string v)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(v));
        }
        

        public List<Kosarkas> KOSARKASI
        {
            get { return lista; }
            set
            {
                if (this.lista != value)
                {
                    this.lista = value;
                    this.NotifyPropertyChanged("KOSARKASI");
                }
            }
        }


        public Kosarkasi()
        {
            this.lista= new List<Kosarkas>();
        }

        public bool dodaj(Kosarkas kosarkas)
        {
            foreach(Kosarkas item in lista)
            {
                if (kosarkas.JMBG == item.JMBG)
                {
                    return false;
                }
            }
            lista.Add(kosarkas);
            return true;
        }

        public bool provera(long jmbg)
        {
            foreach(Kosarkas kosarkas in lista)
            {
                if(kosarkas.JMBG== jmbg)
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
                        lista.Add(new Kosarkas(jmbg, delovi[1], delovi[2], delovi[3],delovi[4], int.Parse(delovi[5]), int.Parse(delovi[6]), double.Parse(delovi[7]), delovi[8]));
                    }

                }
            }catch(Exception ex)
            {

            }
            finally
            {
                if(sr != null) sr.Close();
            }
        }

        public void export(string file)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(file);
                foreach(Kosarkas k in lista)
                {
                    sw.WriteLine(k.ToString());
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                if(sw != null) sw.Close();
            }
        }

        public void exportTabele(string file)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(file);
                sw.WriteLine("Ime ,Prezime ,Pozicija ,Broj poena");
                foreach (Kosarkas k in lista)
                {
                    sw.WriteLine(k.zaTabelu());
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
