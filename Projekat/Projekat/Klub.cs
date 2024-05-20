using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class Klub : INotifyPropertyChanged
    {
        private int id;
        private string naziv;
        private string mesto;
        private string logo;


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string v)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

        public Klub()
        {
            this.id = 0;
            this.naziv=string.Empty;
            this.mesto=string.Empty;    
            this.logo=string.Empty;

        }

        public Klub(int id, string naziv, string mesto, string logo)
        {
            this.id = id;
            this.naziv = naziv;
            this.mesto = mesto;
            this.logo = logo;
        }

        public int ID
        {
            get { return id; }
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.NotifyPropertyChanged("ID");
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
                    this.NotifyPropertyChanged("NAZIV");
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
                    this.NotifyPropertyChanged("MESTO");
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
                    this.NotifyPropertyChanged("LOGO");
                }
            }

        }
    }
}
