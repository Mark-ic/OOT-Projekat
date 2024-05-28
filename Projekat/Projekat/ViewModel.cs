using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Projekat
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Klub> Klubovi { get; set; }
        public ObservableCollection<Klub> KluboviNaMapi { get; set; }

        private Klub _odabraniKlub;

        public ObservableCollection<Kosarkas> Kosarkasi { get; set; }
        public ObservableCollection<Kosarkas> KosarkasiNaTerenu { get; set; }

        private Kosarkas _odabraniKosarkas;
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

        public MainViewModel()
        {
            Klubovi = new ObservableCollection<Klub>();
            KluboviNaMapi = new ObservableCollection<Klub>();
            Kosarkasi=new ObservableCollection<Kosarkas>();
            KosarkasiNaTerenu=new ObservableCollection<Kosarkas>();


        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool dodajKosarkasa(Kosarkas k)
        {
            foreach (Kosarkas item in Kosarkasi)
            {
                if (k.JMBG == item.JMBG)
                {
                    return false;
                }
            }
            Kosarkasi.Add(k);
            return true;
        }
    }
}
